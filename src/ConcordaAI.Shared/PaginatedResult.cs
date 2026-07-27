namespace ConcordaAI.Shared;

public class PaginatedResult<T>
{
    public IEnumerable<T> Itens { get; }
    public int PaginaAtual { get; }
    public int TamanhoPagina { get; }
    public int TotalRegistros { get; }
    public int TotalPaginas => (int)Math.Ceiling(TotalRegistros / (double)TamanhoPagina);

    public PaginatedResult(IEnumerable<T> itens, int totalRegistros, int paginaAtual, int tamanhoPagina)
    {
        Itens = itens;
        PaginaAtual = paginaAtual;
        TotalRegistros = totalRegistros;
        TamanhoPagina = tamanhoPagina;    
    }
}
