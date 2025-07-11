﻿using System.ComponentModel.DataAnnotations;

namespace App.Spec;

public class BaseSpecParams
{
    private const int MaxPageSize = 50;
    [Range(1, int.MaxValue)]
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 20;
    [Range(1, 50)]
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
