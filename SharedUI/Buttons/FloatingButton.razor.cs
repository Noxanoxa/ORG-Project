using Microsoft.AspNetCore.Components;

namespace SharedUI.Buttons;

public partial class FloatingButton
{
    private FloatingPosition position;

    private string style = string.Empty;
    private string buttonClass = string.Empty;
    private ColorTheme color;

    [Parameter] public FloatingPosition Position { get => position; set => setPosition(value); }
    [Parameter] public ColorTheme Color { get => color; set => setColor(value); }
    [Parameter] public EventCallback OnClick { get; set; }
    [Parameter] public string Text { get; set; }

    private void setColor(ColorTheme value)
    {
        color = value;
        buttonClass = value switch
        {
            ColorTheme.Primary => "btn btn-primary",
            ColorTheme.Secondary => "btn btn-secondary",
            ColorTheme.Success => "btn btn-success",
            ColorTheme.Warning => "btn btn-warning",
            ColorTheme.Danger => "btn btn-danger",
            _ => "btn"
        };
    }

    private void setPosition(FloatingPosition value)
    {
        position = value;
        style = value switch
        {
            FloatingPosition.TopLeft => "position:fixed;top:0;margin: 1rem;",
            FloatingPosition.TopRight => "position:fixed;top:0; right:0;margin: 1rem;",
            FloatingPosition.BottomLeft => "position:fixed;bottom:0;margin: 1rem;",
            FloatingPosition.BottomRight => "position:fixed;bottom:0; right:0;margin: 1rem;",
            _ => ""
        };
    }
}