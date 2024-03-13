using Microsoft.AspNetCore.Components;

namespace SharedUI.Cards;

public partial class Card
{
    [Parameter] public bool ShowFooter { get; set; } = true;
    [Parameter] public bool ShowHeader { get; set; } = true;
    [Parameter] public string Title { get; set; }
    [Parameter] public RenderFragment HeaderPills { get; set; }
    [Parameter] public RenderFragment Alert { get; set; }
    [Parameter] public RenderFragment CardBody { get; set; }
    [Parameter] public RenderFragment CardFooter { get; set; }
    [Parameter] public bool ShowBorder { get; set; } = true;
    [Parameter] public bool HasShadow { get; set; } = true;
    [Parameter] public bool Collapse { get; set; } = false;

    private string borderCss => ShowBorder ? "" : "border-0";
    private string shadowCss => HasShadow ? "elevation-2" : "";
    private string headerCss => ShowHeader ? "card-header" : "d-none";
    private bool expanded = true;

    private string cssClass => expanded ? $"card card-sm {borderCss} {shadowCss}" : "d-none";
}