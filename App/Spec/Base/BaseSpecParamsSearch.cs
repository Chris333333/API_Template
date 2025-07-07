namespace App.Spec;
public class BaseSpecParamsSearch : BaseSpecParams
{
    private string? _search;
    public string? Search
    {
        get => _search;
        set => _search = value.ToLower();
    }
}
