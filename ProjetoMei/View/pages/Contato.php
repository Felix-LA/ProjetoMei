<?php
/** Contato: cartões de contato, formulário de mensagem, horários e perguntas frequentes. */
$tituloPagina    = 'Contato - Vitrine MEI';
$descricaoPagina = 'Entre em contato com a Vitrine MEI - Sua plataforma para encontrar profissionais MEI qualificados';
$paginaAtiva     = 'contato';
$estilosExtras   = ['Contato.css'];
$scriptsExtras   = ['Contato.js'];

require_once __DIR__ . '/../routes/Config.php';
require VIEW_PATH . '/components/layout/Topo.php';

$faq = require __DIR__ . '/dados/Faq.php';
$faqEsquerda = array_slice($faq, 0, (int) ceil(count($faq) / 2));
$faqDireita  = array_slice($faq, (int) ceil(count($faq) / 2));

// [ícone, título, linha 1, destaque, observação]
$cartoes = [
    ['fa-phone',        'Telefone', 'Segunda a Sexta: 9h às 18h', '(11) 99999-9999',          'WhatsApp disponível'],
    ['fa-envelope',     'Email',    'Resposta em até 24 horas',   'contato@vitrinemei.com.br', 'suporte@vitrinemei.com.br'],
    ['fa-location-dot', 'Endereço', 'Escritório Central',         'Av. Paulista, 1000',        'São Paulo - SP, 01310-100'],
];

// [título, linha 1, linha 2]
$horarios = [
    ['Atendimento ao Cliente', 'Segunda a Sexta: 9h às 18h', 'Sábado: 9h às 13h'],
    ['Suporte Técnico',        'Segunda a Sexta: 8h às 20h', 'Atendimento emergencial'],
];

$tiposContato = [
    'cliente'  => 'Sou Cliente - Buscando Profissional',
    'mei'      => 'Sou MEI - Quero me Cadastrar',
    'duvida'   => 'Dúvida sobre a Plataforma',
    'sugestao' => 'Sugestão ou Feedback',
    'parceria' => 'Proposta de Parceria',
    'outro'    => 'Outro Assunto',
];

/** Desenha uma coluna de perguntas do FAQ. */
function faq_coluna(array $itens): void
{
    foreach ($itens as $item) { ?>
        <div class="faq-item">
            <button type="button" class="faq-question" aria-expanded="false">
                <span><?= h($item['pergunta']) ?></span>
                <i class="fa-solid fa-chevron-down" aria-hidden="true"></i>
            </button>
            <div class="faq-answer">
                <p><?= nl2br(h($item['resposta'])) ?></p>
            </div>
        </div>
    <?php }
}
?>

<!-- ===== Cabeçalho da página ===== -->
<section class="contact-header">
    <div class="container">
        <h1>Entre em Contato</h1>
        <p class="lead">Estamos aqui para ajudar você a encontrar o profissional MEI ideal ou esclarecer suas dúvidas</p>
    </div>
</section>

<div class="container contato">

    <!-- ===== Cartões de contato ===== -->
    <div class="row g-4 mb-5">
        <?php foreach ($cartoes as [$icone, $titulo, $linha, $destaque, $obs]): ?>
            <div class="col-md-4">
                <div class="contact-info-card text-center">
                    <div class="contact-icon"><i class="fa-solid <?= h($icone) ?>"></i></div>
                    <h4><?= h($titulo) ?></h4>
                    <p><?= h($linha) ?></p>
                    <h5><?= h($destaque) ?></h5>
                    <p class="text-muted mb-0"><?= h($obs) ?></p>
                </div>
            </div>
        <?php endforeach; ?>
    </div>

    <div class="row">
        <!-- ===== Formulário ===== -->
        <div class="col-lg-8">
            <div class="contact-form">
                <h3 class="mb-4">Envie uma Mensagem</h3>

                <div id="contato-aviso" class="alert d-none" role="alert"></div>

                <form id="contact-form" method="post" action="#" novalidate>
                    <div class="row">
                        <div class="col-md-6">
                            <input type="text" class="form-control" id="name" name="name" placeholder="Seu Nome *" aria-label="Seu nome" required>
                        </div>
                        <div class="col-md-6">
                            <input type="email" class="form-control" id="email" name="email" placeholder="Seu Email *" aria-label="Seu e-mail" required>
                        </div>
                    </div>

                    <input type="text" class="form-control" id="subject" name="subject" placeholder="Assunto *" aria-label="Assunto" required>

                    <select class="form-control" id="tipo-contato" name="tipo-contato" aria-label="Tipo de contato" required>
                        <option value="">Selecione o tipo de contato *</option>
                        <?php foreach ($tiposContato as $valor => $rotulo): ?>
                            <option value="<?= h($valor) ?>"><?= h($rotulo) ?></option>
                        <?php endforeach; ?>
                    </select>

                    <textarea class="form-control" id="message" name="message" rows="6" placeholder="Sua Mensagem *" aria-label="Sua mensagem" required></textarea>

                    <div class="form-check mb-4">
                        <input class="form-check-input" type="checkbox" id="newsletter" name="newsletter">
                        <label class="form-check-label" for="newsletter">
                            Desejo receber novidades e ofertas da Vitrine MEI
                        </label>
                    </div>

                    <button type="submit" class="submit-button">
                        <i class="fa-solid fa-paper-plane me-2"></i> Enviar Mensagem
                    </button>
                </form>
            </div>
        </div>

        <!-- ===== Horários ===== -->
        <div class="col-lg-4 mt-5 mt-lg-0">
            <div class="contact-info-card">
                <h4 class="mb-4">Horário de Atendimento</h4>

                <?php foreach ($horarios as [$titulo, $linha1, $linha2]): ?>
                    <div class="mb-4">
                        <h6><?= h($titulo) ?></h6>
                        <p class="mb-2"><?= h($linha1) ?></p>
                        <p class="text-muted"><?= h($linha2) ?></p>
                    </div>
                <?php endforeach; ?>

                <div class="mb-4">
                    <h6>Contato com Profissionais</h6>
                    <p>24 horas via WhatsApp</p>
                    <div class="main-white-button mt-3">
                        <a href="https://wa.me/5511999999999" target="_blank" rel="noopener">
                            <i class="fa-brands fa-whatsapp"></i> Falar no WhatsApp
                        </a>
                    </div>
                </div>

                <div class="social-contact">
                    <a href="#" title="Facebook" aria-label="Facebook"><i class="fa-brands fa-facebook-f"></i></a>
                    <a href="#" title="Instagram" aria-label="Instagram"><i class="fa-brands fa-instagram"></i></a>
                    <a href="#" title="LinkedIn" aria-label="LinkedIn"><i class="fa-brands fa-linkedin-in"></i></a>
                    <a href="#" title="YouTube" aria-label="YouTube"><i class="fa-brands fa-youtube"></i></a>
                </div>
            </div>
        </div>
    </div>

    <!-- ===== Perguntas frequentes ===== -->
    <section class="faq-section">
        <div class="section-heading">
            <h2>Perguntas Frequentes</h2>
            <h6>Encontre respostas rápidas para suas dúvidas</h6>
        </div>

        <div class="row">
            <div class="col-lg-6"><?php faq_coluna($faqEsquerda); ?></div>
            <div class="col-lg-6"><?php faq_coluna($faqDireita); ?></div>
        </div>
    </section>
</div>

<?php require VIEW_PATH . '/components/layout/Rodape.php'; ?>