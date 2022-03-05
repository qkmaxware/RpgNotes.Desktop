window.RpgNotes = (function(){ 
    /**
     * Copy string to clipboard
     * @param {string} input 
     */
    async function CopyToClipboard(input) {
        if (navigator && navigator.clipboard) {
            await navigator.clipboard.writeText(input);
        }
    }

    /**
     * Sets the browser focus to the given element
     * @param {Element} el 
     */
    function SetFocus(el) {
        if (el.focus) {
            el.focus();
        }
    }

    /**
     * Get the start of a selection in an input box
     * @param {input | textarea} input 
     * @returns Number
     */
    function GetSelectionStart(input) {
        return input.selectionStart;
    }

    /**
     * Get the end of a selection in an input box
     * @param {input | textarea} input 
     * @returns Number
     */
    function GetSelectionEnd(input) {
        return input.selectionEnd;
    }

    /**
     * Insert text at the begining of a selector
     * @param {Input Element} el 
     * @param {string} newText
     */
    function InsertAtCursor(el, newText) {
        const start = el.selectionStart;
        const end = el.selectionEnd;
        const text = el.value;
        const before = text.substring(0, start);
        const after  = text.substring(end, text.length);
        el.value = (before + newText + after);
        el.selectionStart = el.selectionEnd = start + newText.length;
        el.focus();
    }

    /**
     * Surround selected text with the given symbols
     * @param {Input Element} el 
     * @param {string} startText 
     * @param {string} endText 
     */
    function SurroundText(el, startText, endText) {
        const start = el.selectionStart;
        const end = el.selectionEnd;
        const text = el.value;
        const before = text.substring(0, start);
        const middle = text.substring(start, end);
        const after  = text.substring(end, text.length);

        el.value = (before + startText + middle + endText + after);
        el.selectionStart = el.selectionEnd = start + startText.length + endText.length;
        el.focus();
    }

    /**
     * Setup a text editor
     * @param {Input Element} el 
     */
    function SetupTextEditor(el, preview) {
        function tabKeyHandler(e) {
            const TABKEY = 9;
            if(e.keyCode == TABKEY) {
                InsertAtCursor(el, "    ");
                if(e.preventDefault) {
                    e.preventDefault();
                }
                return false;
            }
        }

        function sync_scroll() {
            preview.scrollTop = el.scrollTop;
            preview.scrollLeft = el.scrollLeft;
        }

        if(el.addEventListener ) {
            el.addEventListener('keydown', tabKeyHandler, false);
            el.addEventListener('scroll', sync_scroll, false);
        } else if(myInput.attachEvent ) {
            /* IE hack */
            el.attachEvent('onkeydown', tabKeyHandler);
            el.attachEvent('scroll', sync_scroll); 
        }
    }

    /**
     * Fetch the full path of a file in a file input element
     * @param {InputElement} input 
     * @returns the full path to the system file as provided by electron
     */
    function FileInputFullPath(input) {
        if (input && input.files && input.files.length > 0) {
            return input.files[0].path;
        } else {
            return null;
        }
    }

    /**
     * Create an event handler so that when a file is dragged onto the given area, the value of the input element is updated
     * @param {Element} area
     * @param {InputElement} input 
     */
    function InputBindFileDrop(area, input) {
        if (area && input) {
            let disable = ["drag", "dragstart", "dragend", "dragover", "dragenter", "dragleave"];
            for (let i = 0; i < disable.length; i++) {
                area.addEventListener(disable[i], function(e) {
                    e.preventDefault();
                    e.stopPropagation();
                });
            }
            area.addEventListener('drop', function(e) {
                e.preventDefault();
                e.stopPropagation();
                
                let dataTransfer = e.dataTransfer || e.originalEvent.dataTransfer;
                if (dataTransfer) {
                    input.value = dataTransfer.files;
                }
            });
        }
    }

    /**
     * Setup map client interactions for the specific div
     * @param {Element} mapdiv map that actually is moved within the container
     * @param {Element} containerdiv Container in which interactions occur
     */
    function SetupMap(containerdiv, mapdiv) {
        if (containerdiv === null || containerdiv === undefined)
            return;
        if (mapdiv === null || mapdiv === undefined) {
            mapdiv = containerdiv;
        }

        let scrollX = 0;
        let scrollY = 0;
        let zoom = 1;
        let maxZoom = 5;
        let minZoom = 0.1;
        let zoomIncrement = 0.1;

        function setMagnification(level) {
            if (level < minZoom)
                level = minZoom;
            if (level > maxZoom)
                level = maxZoom;

            zoom = level;
            mapdiv.style.transform = "scale(" + zoom + ")";
        }
        function zoomIn() {
            setMagnification(zoom + zoomIncrement);
        }
        function zoomOut() {
            setMagnification(zoom - zoomIncrement);
        }
        function updateScrollPosition(x, y) {
            scrollX = x;
            scrollY = y;
            mapdiv.style.left = scrollX + "px";
            mapdiv.style.top  = scrollY + "px";
        }
        updateScrollPosition(0, 0);

        let client = null;
        let cached = null;
        function dragStart(e) {
            if (e.clientX && e.clientY) {
                if (e.button !== 0) {
                    return; // Left click only
                }
                client = [e.clientX, e.clientY];
                cached = [scrollX, scrollY];
            }
            if (e.touches) {
                var touch = e.touches[0];
                client = [touch.clientX, touch.clientY];
                cached = [scrollX, scrollY];
            }
        }
        function dragMove(e) {
            let src = null;
            if (e.clientX && e.clientY) {
                src = e;
            } else if (e.touches) {
                var touch = e.touches[0];
                src = touch;
            }
            if (src === null) {
                dragEnd(null);
                return;
            }
            
            if (cached === null || client === null)
                return; // Not dragging yet

            let newScrollX = cached[0] + Math.round(src.clientX - client[0]);
            let newScrollY = cached[1] + Math.round(src.clientY - client[1]);
            updateScrollPosition(newScrollX, newScrollY);
        }
        function dragEnd(e) {
            client = null;
            cached = null;
        }
        function getMousePosition(clientX, clientY) {
            var rect = mapdiv.getBoundingClientRect();
            var x = clientX - rect.left; //x position within the element.
            var y = clientY - rect.top;  //y position within the element.

            let invScale = 1.0/zoom;
            let newMouseX = Math.round(x * invScale);
            let newMouseY = Math.round(y * invScale);
            return {
                x: newMouseX,
                y: newMouseY
            };
        }
        function centerOn(x, y) {
            var newX = window.innerWidth/2 - x;
            var newY = window.innerHeight/2 - y;
            updateScrollPosition(newX, newY);
        }

        containerdiv.addEventListener('mousedown', dragStart);
        containerdiv.addEventListener('mousemove', dragMove);
        containerdiv.addEventListener('mouseout', dragEnd);
        containerdiv.addEventListener('mouseup', dragEnd);

        containerdiv.addEventListener('touchstart', dragStart);
        containerdiv.addEventListener('touchmove', dragMove);
        containerdiv.addEventListener('touchleave', dragEnd);
        containerdiv.addEventListener('touchend', dragEnd);

        var instance = {
            setMagnification,
            updateScrollPosition,
            zoomIn,
            zoomOut,
            getMousePosition,
            centerOn
        };
        //mapdiv.map = instance; // Maybe a solution to having "multiple" maps at once
        window.latestMap = instance;
    }
    function MapPanTo(x, y) {
        if (window.latestMap) {
            window.latestMap.updateScrollPosition(x, y);
        }
    }
    function MapSetZoom(level) {
        if (window.latestMap) {
            window.latestMap.setMagnification(level);
        }
    }
    function MapZoomIn() {
        if (window.latestMap) {
            window.latestMap.zoomIn();
        }
    }
    function MapZoomOut() {
        if (window.latestMap) {
            window.latestMap.zoomOut();
        }
    }
    function MapGetPosition(clientX, clientY) {
        if (window.latestMap) {
            return window.latestMap.getMousePosition(clientX, clientY);
        } else {
            return {
                x: clientX,
                y: clientY
            };
        }
    }
    function MapCenterOn(x, y) {
        if (window.latestMap) {
            return window.latestMap.centerOn(x, y);
        } 
    }

    return {
        Editor: {
            Setup: SetupTextEditor,
            GetSelectionStart,
            GetSelectionEnd,
            InsertAtCursor,
            SurroundText,
        },
        FileInputFullPath,
        InputBindFileDrop,
        CopyToClipboard,
        SetFocus,
        Graph: {
            Setup: SetupMap,
        },
        Map: {
            Setup: SetupMap,
            PanTo: MapPanTo,
            SetZoom: MapSetZoom,
            ZoomIn: MapZoomIn,
            ZoomOut: MapZoomOut,
            MousePosition: MapGetPosition,
            Center: MapCenterOn
        }
    };
})();