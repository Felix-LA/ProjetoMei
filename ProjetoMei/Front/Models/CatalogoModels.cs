namespace ProjetoMei.Front.Models
{
    public record CategoriaResumo(string Slug, string Nome, string Opcao, string Icone, string Titulo, string Texto, string Botao, string Imagem, string Alt);

    public record Verificacao(string Icone, string Titulo, string Texto);
    public record NotaRotulo(string Rotulo, string Texto);
    public record Estatistica(string Rotulo, string Valor);
    public record Contratar(string Titulo, string Intro, string[] Passos, NotaRotulo? Dica);

    public record CategoriaDetalhe(
        string Slug, string Nome, string TituloTopo, string Titulo, string Texto, string Destaque,
        string[] Servicos, Verificacao? Verificacao, NotaRotulo? Nota, Estatistica[] Estatisticas, Contratar Contratar);

    public record PerguntaFaq(string Pergunta, string Resposta);

    public record UploadCampo(string Chave, string Rotulo, bool Obrigatorio, string Icone, string Texto, string Dica, string Aceita, string IconeArquivo);
    public record PlanoCadastro(string Chave, string Nome, string Preco, string Sufixo, string Subtitulo, string[] Itens);
    public record Vantagem(string Icone, string Titulo, string Texto);
}
