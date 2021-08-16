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
     * @param {Element} mapdiv 
     */
    function SetupMap(mapdiv) {
        if (mapdiv === null || mapdiv === undefined)
            return;

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

        mapdiv.addEventListener('mousedown', dragStart);
        mapdiv.addEventListener('mousemove', dragMove);
        mapdiv.addEventListener('mouseout', dragEnd);
        mapdiv.addEventListener('mouseup', dragEnd);

        mapdiv.addEventListener('touchstart', dragStart);
        mapdiv.addEventListener('touchmove', dragMove);
        mapdiv.addEventListener('touchleave', dragEnd);
        mapdiv.addEventListener('touchend', dragEnd);

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
        FileInputFullPath,
        InputBindFileDrop,
        CopyToClipboard,
        GetSelectionStart,
        GetSelectionEnd,
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