// <auto-gerado> Convertido de pages/dados/*.php. Para mudar textos, edite aqui. </auto-gerado>
using ProjetoMei.Front.Models;

namespace ProjetoMei.Front.Data
{
    public static class CategoriasDados
    {
        public static readonly IReadOnlyList<CategoriaResumo> Resumos = new List<CategoriaResumo>
        {
            new CategoriaResumo(Slug: "construcao", Nome: "Construção", Opcao: "Construção e Reformas", Icone: "fa-helmet-safety", Titulo: "Profissionais de Construção e Reformas", Texto: "Encontre pedreiros, eletricistas, encanadores, pintores e marceneiros qualificados para seu projeto. Todos os profissionais são MEIs regularizados e com experiência comprovada.", Botao: "Ver Profissionais", Imagem: "https://images.unsplash.com/photo-1504307651254-35680f356dfd?ixlib=rb-1.2.1&auto=format&fit=crop&w=800&h=600&q=80", Alt: "Profissional de construção"),
            new CategoriaResumo(Slug: "tecnologia", Nome: "Tecnologia", Opcao: "Tecnologia e TI", Icone: "fa-laptop-code", Titulo: "Especialistas em Tecnologia", Texto: "Desenvolvedores, programadores, técnicos em informática e consultores em TI. Soluções tecnológicas para seu negócio com qualidade profissional e preço acessível.", Botao: "Explorar", Imagem: "https://images.unsplash.com/photo-1523800503107-5bc3ba2a6f81?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80", Alt: "Profissional de TI"),
            new CategoriaResumo(Slug: "design", Nome: "Design", Opcao: "Design e Marketing", Icone: "fa-palette", Titulo: "Designers e Marketing Digital", Texto: "Designers gráficos, social media, redatores e especialistas em marketing digital. Destaque seu negócio com profissionais criativos e atualizados.", Botao: "Ver Portfólios", Imagem: "https://images.unsplash.com/photo-1561070791-2526d30994b5?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80", Alt: "Designer trabalhando"),
            new CategoriaResumo(Slug: "consultoria", Nome: "Consultoria", Opcao: "Consultoria", Icone: "fa-chart-column", Titulo: "Consultores e Assessores", Texto: "Consultores financeiros, contadores MEI, coaches e especialistas em diversas áreas. Obtenha orientação profissional para alavancar seu negócio.", Botao: "Saber Mais", Imagem: "https://images.unsplash.com/photo-1552664730-d307ca884978?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80", Alt: "Consultoria"),
            new CategoriaResumo(Slug: "servicos", Nome: "Serviços", Opcao: "Serviços Domésticos", Icone: "fa-screwdriver-wrench", Titulo: "Serviços Diversos", Texto: "Fotógrafos, organizadores, limpeza, manutenção e muito mais. Encontre profissionais para todas as necessidades do seu dia a dia ou negócio.", Botao: "Descobrir", Imagem: "https://images.unsplash.com/photo-1581578731548-c64695cc6952?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80", Alt: "Serviços diversos"),
        };

        public static readonly IReadOnlyList<CategoriaDetalhe> Detalhes = new List<CategoriaDetalhe>
        {
            new CategoriaDetalhe(
                Slug: "construcao", Nome: "Construção", TituloTopo: "Construção e Reformas",
                Titulo: "Profissionais de Construção e Reformas",
                Texto: "Encontre profissionais qualificados para todos os tipos de serviços de construção, reforma e manutenção residencial ou comercial. Todos os profissionais são MEIs regularizados com experiência comprovada e documentos em dia.",
                Destaque: "Serviços Mais Procurados:",
                Servicos: new string[] { "Marcenaria e Móveis", "Pedreiro e Alvenaria", "Instalação Elétrica", "Encanamento e Hidráulica", "Pintura Residencial", "Instalação de Piso", "Reformas de Cozinha", "Construção de Muro", "Reparos em Telhado", "Acabamentos em Gesso" },
                Verificacao: new Verificacao("fa-shield-halved", "Verificação de Profissionais", "Todos os profissionais desta categoria passam por verificação de documentos, incluindo CNPJ MEI ativo, experiência comprovada e avaliações de clientes anteriores."),
                Nota: null,
                Estatisticas: new Estatistica[] { new Estatistica("Avaliação Média", "4.7/5.0"), new Estatistica("Taxa de Sucesso", "96%"), new Estatistica("Tempo Médio de Resposta", "2 horas"), new Estatistica("Projetos Concluídos", "12.500+"), new Estatistica("Orçamentos Gratuitos", "Sim") },
                Contratar: new Contratar(Titulo: "Como Contratar Profissionais de Construção", Intro: "Para garantir a melhor experiência ao contratar um profissional MEI de construção:", Passos: new string[] { "Solicite orçamentos detalhados de pelo menos 3 profissionais", "Verifique as avaliações e portfólio de trabalhos anteriores", "Peça referências e contato de clientes anteriores", "Confirme se o profissional possui os equipamentos necessários", "Estabeleça um contrato claro com prazos e valores", "Utilize nossa plataforma para acompanhar o progresso do projeto", "Avalie o profissional após a conclusão do serviço" }, Dica: new NotaRotulo("Dica", "Profissionais com selo \"Premium\" possuem seguro de responsabilidade civil e garantia estendida nos serviços."))),
            new CategoriaDetalhe(
                Slug: "tecnologia", Nome: "Tecnologia", TituloTopo: "Tecnologia e TI",
                Titulo: "Especialistas em Tecnologia da Informação",
                Texto: "Profissionais especializados em desenvolvimento de software, suporte técnico, infraestrutura de TI e soluções digitais para seu negócio. Trabalho remoto e presencial disponível.",
                Destaque: "Habilidades em Destaque:",
                Servicos: new string[] { "Desenvolvimento Web", "Apps Mobile", "WordPress e CMS", "Suporte Técnico", "Redes e Infraestrutura", "E-commerce", "SEO e Marketing Digital", "Banco de Dados", "Automação", "Consultoria em TI" },
                Verificacao: null,
                Nota: new NotaRotulo("Tecnologias mais requisitadas", "React, Node.js, Python, PHP, WordPress, Flutter, AWS, Google Cloud"),
                Estatisticas: new Estatistica[] { new Estatistica("Avaliação Média", "4.8/5.0"), new Estatistica("Projetos Remotos", "85%"), new Estatistica("Entrega no Prazo", "94%"), new Estatistica("Especializações", "15+ áreas"), new Estatistica("Suporte Pós-Projeto", "Sim") },
                Contratar: new Contratar(Titulo: "Informações para Contratação", Intro: "Para projetos de tecnologia, recomendamos:", Passos: new string[] { "Defina claramente o escopo do projeto", "Solicite portfólios e casos de sucesso", "Verifique certificações e habilidades técnicas", "Estabeleça metodologia de trabalho (ágil, cascata)", "Defina marcos de entrega e revisões", "Acorde sobre manutenção pós-lançamento", "Utilize nossa plataforma para versionamento" }, Dica: null)),
            new CategoriaDetalhe(
                Slug: "design", Nome: "Design", TituloTopo: "Design e Marketing",
                Titulo: "Criativos e Estrategistas Digitais",
                Texto: "Designers gráficos, especialistas em marketing digital, redatores e produtores de conteúdo para elevar sua marca e presença digital.",
                Destaque: "Serviços Criativos:",
                Servicos: new string[] { "Design Gráfico", "Identidade Visual", "Social Media", "Redação Publicitária", "Ilustração Digital", "Marketing Digital", "Gestão de Tráfego", "Produção de Vídeo", "Fotografia Profissional", "Email Marketing" },
                Verificacao: null,
                Nota: new NotaRotulo("Ferramentas populares", "Adobe Creative Suite, Figma, Canva, Google Analytics, Meta Business Suite"),
                Estatisticas: new Estatistica[] { new Estatistica("Avaliação Média", "4.6/5.0"), new Estatistica("Entregas Criativas", "8.200+"), new Estatistica("Briefings em 24h", "92%"), new Estatistica("Revisões Incluídas", "3 por projeto"), new Estatistica("Formatos Suportados", "20+") },
                Contratar: new Contratar(Titulo: "Diretrizes para Projetos Criativos", Intro: "Para garantir o sucesso do seu projeto de design:", Passos: new string[] { "Prepare um briefing detalhado", "Forneça referências visuais", "Defina público-alvo e objetivos", "Estabeleça cronograma de entregas", "Especifique formatos necessários", "Defina direitos autorais e uso", "Agende reuniões de acompanhamento" }, Dica: null)),
            new CategoriaDetalhe(
                Slug: "consultoria", Nome: "Consultoria", TituloTopo: "Consultoria e Assessoria",
                Titulo: "Especialistas em Consultoria Empresarial",
                Texto: "Consultores especializados em diversas áreas para impulsionar seu negócio. Desde consultoria financeira até especialistas em recursos humanos e gestão empresarial.",
                Destaque: "Áreas de Consultoria:",
                Servicos: new string[] { "Contabilidade MEI", "Consultoria Financeira", "Gestão Empresarial", "Recursos Humanos", "Planejamento Estratégico", "Compliance e Legal", "Marketing Estratégico", "Consultoria de Vendas", "Análise de Mercado", "Transformação Digital" },
                Verificacao: null,
                Nota: null,
                Estatisticas: new Estatistica[] { new Estatistica("Avaliação Média", "4.9/5.0"), new Estatistica("Certificações", "95% possuem"), new Estatistica("Experiência Média", "12 anos"), new Estatistica("Clientes Atendidos", "3.500+"), new Estatistica("Consultorias Online", "70%") },
                Contratar: new Contratar(Titulo: "Processo de Consultoria", Intro: "Para maximizar os resultados da consultoria:", Passos: new string[] { "Agende sessão de diagnóstico inicial", "Defina objetivos mensuráveis", "Estabeleça plano de ação", "Programe sessões de acompanhamento", "Documente recomendações", "Implemente mudanças gradualmente", "Avalie resultados periodicamente" }, Dica: null)),
            new CategoriaDetalhe(
                Slug: "servicos", Nome: "Serviços", TituloTopo: "Serviços Diversos",
                Titulo: "Serviços Especializados para Todas as Necessidades",
                Texto: "Profissionais para serviços domésticos, eventos, saúde, educação e diversas outras necessidades do dia a dia pessoal e empresarial.",
                Destaque: "Serviços Disponíveis:",
                Servicos: new string[] { "Limpeza Residencial", "Organização de Espaços", "Serviços de Buffet", "Cuidador de Idosos", "Aulas Particulares", "Fotografia de Eventos", "Manutenção de Jardim", "Transporte de Pequenas Cargas", "Serviços de Costura", "Instalação de Ar Condicionado" },
                Verificacao: new Verificacao("fa-certificate", "Profissionais Verificados", "Todos os profissionais desta categoria possuem certificados específicos quando aplicável (como manipuladores de alimentos, cuidadores, etc.) e passam por verificação de antecedentes."),
                Nota: null,
                Estatisticas: new Estatistica[] { new Estatistica("Avaliação Média", "4.7/5.0"), new Estatistica("Agendamento Online", "100%"), new Estatistica("Disponibilidade", "24/7 para alguns"), new Estatistica("Serviços Urgentes", "65% oferecem"), new Estatistica("Área de Atuação", "Todo Brasil") },
                Contratar: new Contratar(Titulo: "Informações para Contratação de Serviços", Intro: "Para serviços diversos, recomendamos:", Passos: new string[] { "Verifique disponibilidade imediata", "Confirme equipamentos necessários", "Solicite orçamento detalhado", "Estabeleça horários e prazos", "Verifique certificações específicas", "Confirme seguro quando aplicável", "Utilize nossa plataforma para pagamento seguro" }, Dica: new NotaRotulo("Importante", "Para serviços que envolvem acesso à residência, nossos profissionais passam por verificação de antecedentes mais rigorosa."))),
        };
    }

    public static class FaqDados
    {
        public static readonly IReadOnlyList<PerguntaFaq> Perguntas = new List<PerguntaFaq>
        {
            new PerguntaFaq("Como cadastrar meu MEI na plataforma?", "Para cadastrar seu MEI, clique em \"Cadastrar MEI\" no menu superior. Você precisará fornecer seus dados pessoais, CNPJ MEI, comprovante de endereço e uma descrição dos seus serviços. O cadastro é gratuito e leva cerca de 10 minutos."),
            new PerguntaFaq("Como faço para contratar um profissional?", "Navegue pelas categorias ou use a busca para encontrar o profissional desejado. Clique no perfil e use o botão \"Solicitar Orçamento\" para entrar em contato diretamente com o profissional via WhatsApp ou email. Você pode solicitar orçamentos de vários profissionais para comparar."),
            new PerguntaFaq("A BuscaMei cobra alguma taxa?", "Para clientes: O uso da plataforma é totalmente gratuito. Você não paga nada para buscar e entrar em contato com profissionais.\nPara MEIs: O cadastro básico é gratuito. Oferecemos planos premium com benefícios adicionais, mas o plano gratuito já permite divulgar seus serviços."),
            new PerguntaFaq("Como funciona o sistema de avaliações?", "Após a conclusão de um serviço, tanto o cliente quanto o profissional podem se avaliar mutuamente. As avaliações são verificadas e apenas usuários que realmente realizaram transações podem avaliar. Isso garante a confiabilidade do sistema."),
            new PerguntaFaq("Posso cancelar um serviço contratado?", "O cancelamento depende do acordo estabelecido diretamente com o profissional. Recomendamos sempre formalizar os termos do serviço antes do início. Em caso de problemas, nossa equipe de mediação pode auxiliar na resolução de conflitos."),
            new PerguntaFaq("Como denunciar um profissional?", "Caso tenha problemas com algum profissional, entre em contato conosco pelo email suporte@buscamei.com.br ou pelo WhatsApp (11) 99999-9999. Forneça todas as informações possíveis sobre o ocorrido. Investigaremos a situação e tomaremos as medidas necessárias."),
        };
    }

    public static class CadastroDados
    {
        public static readonly IReadOnlyList<KeyValuePair<string, string>> Estados = new List<KeyValuePair<string, string>>
        {
            new("AC", "Acre"),
            new("AL", "Alagoas"),
            new("AP", "Amapá"),
            new("AM", "Amazonas"),
            new("BA", "Bahia"),
            new("CE", "Ceará"),
            new("DF", "Distrito Federal"),
            new("ES", "Espírito Santo"),
            new("GO", "Goiás"),
            new("MA", "Maranhão"),
            new("MT", "Mato Grosso"),
            new("MS", "Mato Grosso do Sul"),
            new("MG", "Minas Gerais"),
            new("PA", "Pará"),
            new("PB", "Paraíba"),
            new("PR", "Paraná"),
            new("PE", "Pernambuco"),
            new("PI", "Piauí"),
            new("RJ", "Rio de Janeiro"),
            new("RN", "Rio Grande do Norte"),
            new("RS", "Rio Grande do Sul"),
            new("RO", "Rondônia"),
            new("RR", "Roraima"),
            new("SC", "Santa Catarina"),
            new("SP", "São Paulo"),
            new("SE", "Sergipe"),
            new("TO", "Tocantins"),
        };

        public static readonly IReadOnlyList<KeyValuePair<string, string>> Categorias = new List<KeyValuePair<string, string>>
        {
            new("construcao", "🏗️ Construção"),
            new("tecnologia", "💻 Tecnologia"),
            new("design", "🎨 Design"),
            new("consultoria", "📊 Consultoria"),
            new("servicos", "🔧 Serviços"),
            new("eletricista", "⚡ Eletricista"),
            new("encanador", "🔧 Encanador"),
            new("pintor", "🎨 Pintor"),
            new("marceneiro", "🪚 Marceneiro"),
            new("programador", "👨‍💻 Programador"),
            new("designer", "✏️ Designer"),
            new("fotografo", "📸 Fotógrafo"),
            new("contador", "📊 Contador"),
        };

        public static readonly IReadOnlyList<string> Requisitos = new List<string>
        {
            "CNPJ MEI ativo",
            "Documento com foto (RG ou CNH)",
            "Comprovante de residência",
            "Foto do rosto (para perfil)",
        };

        public static readonly IReadOnlyList<UploadCampo> Uploads = new List<UploadCampo>
        {
            new UploadCampo(Chave: "doc", Rotulo: "Documento de Identificação (RG ou CNH)", Obrigatorio: true, Icone: "fa-id-card", Texto: "Clique para enviar foto ou PDF", Dica: "Apenas arquivos JPG, PNG ou PDF (máx. 5MB)", Aceita: ".jpg,.jpeg,.png,.pdf", IconeArquivo: "fa-file-lines"),
            new UploadCampo(Chave: "comprovante", Rotulo: "Comprovante de Residência", Obrigatorio: true, Icone: "fa-house", Texto: "Clique para enviar comprovante", Dica: "Conta de luz, água ou telefone (máx. 5MB)", Aceita: ".jpg,.jpeg,.png,.pdf", IconeArquivo: "fa-file-lines"),
            new UploadCampo(Chave: "foto", Rotulo: "Foto para Perfil", Obrigatorio: true, Icone: "fa-camera", Texto: "Clique para enviar sua foto", Dica: "Foto nítida do rosto (máx. 5MB)", Aceita: ".jpg,.jpeg,.png", IconeArquivo: "fa-file-image"),
            new UploadCampo(Chave: "cnpj", Rotulo: "Comprovante de CNPJ (opcional)", Obrigatorio: false, Icone: "fa-file-invoice", Texto: "Clique para enviar comprovante", Dica: "Certificado ou extrato CNPJ (máx. 5MB)", Aceita: ".jpg,.jpeg,.png,.pdf", IconeArquivo: "fa-file-lines"),
        };

        public static readonly IReadOnlyList<PlanoCadastro> Planos = new List<PlanoCadastro>
        {
            new PlanoCadastro(Chave: "gratuito", Nome: "Plano Básico", Preco: "GRÁTIS", Sufixo: "", Subtitulo: "Ideal para começar", Itens: new string[] { "Perfil na plataforma", "Até 5 propostas/mês", "Avaliações de clientes", "Verificação básica" }),
            new PlanoCadastro(Chave: "premium", Nome: "Plano Premium", Preco: "R$ 29,90", Sufixo: "/mês", Subtitulo: "Para mais resultados", Itens: new string[] { "Tudo do Plano Básico", "Propostas ilimitadas", "Destaque nas buscas", "Perfil verificado", "Suporte prioritário", "Estatísticas detalhadas" }),
        };

        public static readonly IReadOnlyList<Vantagem> Porque = new List<Vantagem>
        {
            new Vantagem("fa-bullseye", "Alcance mais clientes", "Sua vitrine online 24 horas"),
            new Vantagem("fa-shield-halved", "Perfil verificado", "Ganhe a confiança dos clientes"),
            new Vantagem("fa-star", "Avaliações reais", "Mostre seu trabalho de qualidade"),
            new Vantagem("fa-bolt", "Rápido e fácil", "Comece a receber propostas em 24h"),
        };
    }
}
