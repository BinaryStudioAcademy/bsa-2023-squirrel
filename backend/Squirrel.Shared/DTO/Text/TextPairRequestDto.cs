﻿namespace Squirrel.Shared.DTO.Text;

public class TextPairRequestDto
{
    public string? OldText { get; set; }
    public string? NewText { get; set; }
    public bool IgnoreWhitespace { get; set; } = false;
}
