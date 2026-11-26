using ProjetoMei.Back.Models;
using ProjetoMei.Back.Services;

namespace ProjetoMei.Back.Data
{
    /// <summary>
    /// Dados de exemplo (mocados): 5 MEIs aprovados em cada uma das 13 categorias = 65 cadastros,
    /// para a vitrine e as abas de profissionais não ficarem vazias.
    /// Liga/desliga em "Seed:DadosExemplo" no appsettings.json. Só roda se ainda não existirem.
    /// Senha de todos: Exemplo@123 (e-mails terminam em @exemplo.buscamei.com).
    /// </summary>
    public static class DadosExemplo
    {
        public const string DominioEmail = "@exemplo.buscamei.com";
        private const string SenhaPadrao = "Exemplo@123";

        private record Categoria(string Chave, string Ramo, string Servicos, string[] Nomes);

        private static readonly Categoria[] Categorias =
        {
            new("construcao", "Construção e Reformas", "alvenaria, reformas, acabamento e pequenas obras",
                new[] { "Carlos Eduardo Menezes", "Antônio Barbosa Lima", "José Roberto Pacheco", "Marcos Vinícius Duarte", "Valdemir Souza Ramos" }),
            new("tecnologia", "Tecnologia e TI", "suporte técnico, redes, manutenção de computadores e criação de sites",
                new[] { "Rafael Augusto Teixeira", "Bruna Carolina Nogueira", "Diego Fernandes Cruz", "Larissa Mendes Prado", "Thiago Henrique Veloso" }),
            new("design", "Design e Marketing", "identidade visual, gestão de redes sociais e material gráfico",
                new[] { "Juliana Prado Siqueira", "Fernanda Lopes Cardoso", "Gabriel Torres Amaral", "Camila Ribeiro Neves", "Leonardo Batista Reis" }),
            new("consultoria", "Consultoria", "planejamento financeiro, gestão de pequenos negócios e abertura de empresas",
                new[] { "Patrícia Moura Andrade", "Ricardo Alves Fontes", "Sandra Regina Coelho", "Eduardo Matos Figueira", "Luciana Barros Tavares" }),
            new("servicos", "Serviços Domésticos", "limpeza, diarista, jardinagem e pequenos reparos",
                new[] { "Maria das Graças Oliveira", "Aparecida Ferreira Dias", "Joana Darc Monteiro", "Rosângela Pires Campos", "Edson Luiz Carvalho" }),
            new("eletricista", "Eletricista", "instalações elétricas, troca de fiação, quadros de energia e iluminação",
                new[] { "Paulo César Rodrigues", "Adriano Martins Leal", "Wellington Costa Freitas", "Jorge Luís Ferraz", "Sérgio Ricardo Bastos" }),
            new("encanador", "Encanador", "vazamentos, desentupimentos, instalação de louças e caixas d'água",
                new[] { "Francisco de Assis Lima", "Nilson Gomes Pereira", "Rogério Dias Cavalcante", "Cláudio Moreira Santos", "Anderson Pinto Valente" }),
            new("pintor", "Pintor", "pintura residencial e comercial, textura, grafiato e massa corrida",
                new[] { "Reinaldo Silva Machado", "Fábio Henrique Moraes", "Osvaldo Cunha Rezende", "Alessandro Vieira Borges", "Gilberto Azevedo Nunes" }),
            new("marceneiro", "Marceneiro", "móveis planejados, armários sob medida, reparos e restauração",
                new[] { "Armando Pereira Guedes", "Vanderlei Costa Brandão", "Moacir Santana Rocha", "Délcio Ferreira Matos", "Ivan Rodrigo Albuquerque" }),
            new("programador", "Programador", "sistemas web, aplicativos, automações e integração com APIs",
                new[] { "Lucas Gabriel Ferreira", "Mariana Alves Duarte", "Pedro Henrique Salles", "Isabela Cristina Rangel", "Vinícius Moreira Lacerda" }),
            new("designer", "Designer", "logotipos, embalagens, cartões de visita e peças para redes sociais",
                new[] { "Natália Brandão Esteves", "Henrique Dantas Freire", "Bianca Louise Carneiro", "Matheus Oliveira Sampaio", "Tatiane Gouveia Ramos" }),
            new("fotografo", "Fotógrafo", "ensaios, eventos, fotos de produtos e cobertura de aniversários e casamentos",
                new[] { "Rodrigo Cesar Albuquerque", "Aline Cristina Furtado", "Felipe Santos Medeiros", "Débora Lima Vasconcelos", "Gustavo Henrique Pinheiro" }),
            new("contador", "Contador", "abertura e regularização de MEI, declaração anual, impostos e folha",
                new[] { "Simone Aparecida Rezende", "Wagner Luiz Cordeiro", "Renata Gonçalves Fagundes", "Alexandre Nogueira Brito", "Cristiane Maia Teles" }),
        };

        // Cidades (cidade, UF) alternadas entre os 5 MEIs de cada categoria.
        private static readonly (string Cidade, string Uf)[] Cidades =
        {
            ("Campo Grande", "MS"), ("Dourados", "MS"), ("Três Lagoas", "MS"), ("Corumbá", "MS"), ("Cuiabá", "MT"),
        };

        // Experiência (mesmas chaves do cadastro) e plano de cada um dos 5.
        private static readonly string[] Experiencias = { "5-10", "10+", "3-5", "1-3", "0-1" };

        public static void Semear(MeiDbContext db)
        {
            if (db.Meis.Any(m => m.Email.EndsWith(DominioEmail)))
            {
                return;
            }

            // Um hash só para todos (PBKDF2 é lento de propósito; os 65 compartilham a mesma senha).
            string hash = SenhaHasher.Gerar(SenhaPadrao);
            DateTime agora = DateTime.UtcNow;
            int n = 0;

            foreach (Categoria cat in Categorias)
            {
                for (int i = 0; i < cat.Nomes.Length; i++)
                {
                    n++;
                    string nome = cat.Nomes[i];
                    (string cidade, string uf) = Cidades[(i + n) % Cidades.Length];
                    string exp = Experiencias[i];
                    string primeiroNome = nome.Split(' ')[0];

                    db.Meis.Add(new UsuarioMeiModel
                    {
                        Nome = nome,
                        Cpf = $"9000000{n:0000}",
                        Cnpj = $"1000000000{n:0000}",
                        Email = $"{cat.Chave}{i + 1}{DominioEmail}",
                        Telefone = $"6799{n:0000}{i:000}"[..11],
                        Cidade = cidade,
                        Estado = uf,
                        RamoAtividade = cat.Ramo,
                        Descricao = $"{primeiroNome} atende em {cidade} e região com {cat.Servicos}. " +
                                    "Orçamento sem compromisso, pontualidade e serviço com garantia.",
                        Experiencia = exp,
                        Portfolio = i % 2 == 0 ? $"https://exemplo.com.br/{cat.Chave}/{i + 1}" : string.Empty,
                        Categorias = cat.Chave,
                        Plano = i == 0 ? "premium" : "gratuito",
                        Status = StatusCadastro.Aprovado,
                        DataAnalise = agora,
                        DataCadastro = agora.AddMinutes(-n),
                        SenhaHash = hash
                    });
                }
            }

            db.SaveChanges();
        }
    }
}