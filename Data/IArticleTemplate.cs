namespace RpgNotes.Desktop.Data {

public interface IArticleTemplate {
    string TemplateName();
    Image TemplateIcon();
    string TemplateHint();
    Article Create(RpgSystem system);
}

}