using Microsoft.AspNetCore.Components;

namespace SharedUI.Cards;

public partial class CollapseCard : ComponentBase
{
    [Parameter] public RenderFragment CardBody { get; set; }
    [Parameter] public RenderFragment CardFooter { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public string Accordion { get; set; } = "accordion";
    [Parameter] public bool Collapsed { get; set; } = true;
    [Parameter] public bool ShowFooter { get; set; } = true;

    private string headingId;
    private string bodyId;
    private string bodyTarget;
    private string accordionName => $"#{Accordion}";

    private string collapseCss => Collapsed ? "collapse" : "collapse show";

    protected override void OnInitialized()
    {
        int id = new Random().Next(1, 1000);
        bodyId = $"collapse{new Random().Next(1, 1000)}";
        headingId = $"heading{new Random().Next(1, 1000)}";
        bodyTarget = $"#{bodyId}";
    }
}