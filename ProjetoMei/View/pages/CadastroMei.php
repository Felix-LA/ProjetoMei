<?php
/** Cadastro de MEI em 4 passos: dados pessoais, informações profissionais, documentos e plano. */
$tituloPagina    = 'Cadastrar MEI - Vitrine MEI';
$descricaoPagina = 'Cadastre seu MEI na Vitrine MEI e comece a receber propostas de trabalho agora mesmo';
$paginaAtiva     = '';
$estilosExtras   = ['CadastroMei.css'];
$scriptsExtras   = ['CadastroMei.js'];

require_once __DIR__ . '/../routes/Config.php';
require VIEW_PATH . '/components/layout/Topo.php';

$dados   = require __DIR__ . '/dados/CadastroMei.php';
$estados = $dados['estados'];

$passos = ['Dados Pessoais', 'Informações Profissionais', 'Documentos', 'Plano'];
?>

<!-- ===== Cabeçalho da página ===== -->
<section class="cadastro-header">
    <div class="container">
        <h1>Cadastre Seu MEI</h1>
        <p class="lead">Aumente sua visibilidade e receba propostas de trabalho diariamente</p>
        <p>Cadastro 100% gratuito &bull; Até 5x mais clientes &bull; Perfil verificado</p>
    </div>
</section>

<div class="container cadastro" data-cadastro data-destino="<?= h(rota('inicio')) ?>">

    <!-- ===== Indicador de passos ===== -->
    <div class="step-indicator" id="indicador-passos">
        <?php foreach ($passos as $i => $nome): ?>
            <div class="step <?= $i === 0 ? 'active' : '' ?>" id="step<?= $i + 1 ?>-indicator">
                <div class="step-number"><?= $i + 1 ?></div>
                <div class="step-text"><?= h($nome) ?></div>
            </div>
        <?php endforeach; ?>
    </div>

    <div id="cadastro-aviso" class="alert alert-danger d-none" role="alert"></div>

    <form id="cadastro-mei-form" method="post" action="<?= h(rota('cadastro-mei.salvar')) ?>" novalidate>
        <input type="hidden" name="csrf" value="<?= h(csrf_token()) ?>">

        <!-- ===== Passo 1: Dados pessoais ===== -->
        <div class="form-section form-step active" id="step1">
            <h3 class="mb-4">Dados Pessoais</h3>

            <div class="row">
                <div class="col-md-6 form-group">
                    <label class="form-label" for="nome">Nome Completo *</label>
                    <input type="text" class="form-control" id="nome" name="nome" required>
                </div>
                <div class="col-md-6 form-group">
                    <label class="form-label" for="cpf">CPF *</label>
                    <input type="text" class="form-control" id="cpf" name="cpf" placeholder="000.000.000-00" inputmode="numeric" required>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 form-group">
                    <label class="form-label" for="email">E-mail *</label>
                    <input type="email" class="form-control" id="email" name="email" required>
                </div>
                <div class="col-md-6 form-group">
                    <label class="form-label" for="telefone">Telefone/WhatsApp *</label>
                    <input type="tel" class="form-control" id="telefone" name="telefone" required>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 form-group">
                    <label class="form-label" for="cidade">Cidade *</label>
                    <input type="text" class="form-control" id="cidade" name="cidade" required>
                </div>
                <div class="col-md-6 form-group">
                    <label class="form-label" for="estado">Estado *</label>
                    <select class="form-control" id="estado" name="estado" required>
                        <option value="">Selecione</option>
                        <?php foreach ($estados as $sigla => $nome): ?>
                            <option value="<?= h($sigla) ?>"><?= h($nome) ?></option>
                        <?php endforeach; ?>
                    </select>
                </div>
            </div>

            <div class="form-group">
                <div class="checkbox-group">
                    <input type="checkbox" id="termos" name="termos" required>
                    <label for="termos">Li e aceito os
                        <a href="<?= rota('termos') ?>" target="_blank" rel="noopener">Termos de Uso</a> e
                        <a href="<?= rota('privacidade') ?>" target="_blank" rel="noopener">Política de Privacidade</a> *</label>
                </div>
            </div>

            <div class="text-end">
                <button type="button" class="btn-next" data-passo-proximo="2">Próximo <i class="fa-solid fa-arrow-right ms-2"></i></button>
            </div>
        </div>

        <!-- ===== Passo 2: Informações profissionais ===== -->
        <div class="form-section form-step" id="step2">
            <h3 class="mb-4">Informações Profissionais</h3>

            <div class="form-group">
                <label class="form-label">Selecione suas Categorias *</label>
                <div class="categorias-container" id="categorias-container">
                    <?php foreach ($dados['categorias'] as [$valor, $rotulo]): ?>
                        <button type="button" class="categoria-tag" data-value="<?= h($valor) ?>" aria-pressed="false"><?= h($rotulo) ?></button>
                    <?php endforeach; ?>
                </div>
                <input type="hidden" id="categorias" name="categorias">
            </div>

            <div class="form-group">
                <label class="form-label" for="cnpj">CNPJ MEI *</label>
                <input type="text" class="form-control" id="cnpj" name="cnpj" placeholder="00.000.000/0001-00" inputmode="numeric" required>
                <small class="text-muted">Deve ser um CNPJ MEI ativo na Receita Federal</small>
            </div>

            <div class="form-group">
                <label class="form-label" for="ramo-atividade">Ramo de Atividade (CNAE) *</label>
                <input type="text" class="form-control" id="ramo-atividade" name="ramo-atividade" required>
            </div>

            <div class="form-group">
                <label class="form-label" for="descricao">Descrição dos Serviços *</label>
                <textarea class="form-control" id="descricao" name="descricao" rows="4" placeholder="Descreva os serviços que você oferece, sua experiência, especializações..." required></textarea>
                <small class="text-muted">Esta descrição aparecerá no seu perfil para os clientes</small>
            </div>

            <div class="form-group">
                <label class="form-label" for="experiencia">Anos de Experiência</label>
                <select class="form-control" id="experiencia" name="experiencia">
                    <option value="">Selecione</option>
                    <option value="0-1">Menos de 1 ano</option>
                    <option value="1-3">1 a 3 anos</option>
                    <option value="3-5">3 a 5 anos</option>
                    <option value="5-10">5 a 10 anos</option>
                    <option value="10+">Mais de 10 anos</option>
                </select>
            </div>

            <div class="form-group">
                <label class="form-label" for="portfolio">Link do Portfólio (opcional)</label>
                <input type="url" class="form-control" id="portfolio" name="portfolio" placeholder="https://seusite.com ou link do LinkedIn">
            </div>

            <div class="d-flex justify-content-between">
                <button type="button" class="btn-prev" data-passo-anterior="1"><i class="fa-solid fa-arrow-left me-2"></i> Voltar</button>
                <button type="button" class="btn-next" data-passo-proximo="3">Próximo <i class="fa-solid fa-arrow-right ms-2"></i></button>
            </div>
        </div>

        <!-- ===== Passo 3: Documentos ===== -->
        <div class="form-section form-step" id="step3">
            <h3 class="mb-4">Documentos para Verificação</h3>
            <p class="text-muted mb-4">Para garantir a segurança e confiabilidade da nossa plataforma, precisamos verificar seus documentos. Todos os dados são protegidos e usados apenas para validação.</p>

            <div class="requisitos-box mb-4">
                <h5>Documentos Necessários:</h5>
                <?php foreach ($dados['requisitos'] as $requisito): ?>
                    <div class="requisito-item">
                        <i class="fa-solid fa-circle-check text-success me-2"></i> <?= h($requisito) ?>
                    </div>
                <?php endforeach; ?>
            </div>

            <div class="row">
                <?php foreach ($dados['uploads'] as $u): $k = $u['chave']; ?>
                    <div class="col-md-6 form-group">
                        <label class="form-label" for="file-<?= h($k) ?>"><?= h($u['rotulo']) ?><?= $u['obrigatorio'] ? ' *' : '' ?></label>

                        <div class="file-upload" data-arquivo-abrir="<?= h($k) ?>" role="button" tabindex="0">
                            <i class="fa-solid <?= h($u['icone']) ?>"></i>
                            <p><?= h($u['texto']) ?></p>
                            <small><?= h($u['dica']) ?></small>
                        </div>

                        <input type="file" id="file-<?= h($k) ?>" class="d-none" accept="<?= h($u['aceita']) ?>"
                               data-arquivo="<?= h($k) ?>"<?= $u['obrigatorio'] ? ' data-obrigatorio' : '' ?>>

                        <div id="<?= h($k) ?>-uploaded" class="uploaded-file d-none">
                            <span><i class="fa-solid <?= h($u['icone_arquivo']) ?> me-2"></i> <span id="<?= h($k) ?>-name"></span></span>
                            <button type="button" class="btn btn-sm btn-outline-danger" data-arquivo-remover="<?= h($k) ?>">Remover</button>
                        </div>
                    </div>
                <?php endforeach; ?>
            </div>

            <div class="d-flex justify-content-between">
                <button type="button" class="btn-prev" data-passo-anterior="2"><i class="fa-solid fa-arrow-left me-2"></i> Voltar</button>
                <button type="button" class="btn-next" data-passo-proximo="4">Próximo <i class="fa-solid fa-arrow-right ms-2"></i></button>
            </div>
        </div>

        <!-- ===== Passo 4: Plano ===== -->
        <div class="form-section form-step" id="step4">
            <h3 class="mb-4">Escolha Seu Plano</h3>
            <p class="text-muted mb-4">Comece gratuitamente e atualize quando quiser</p>

            <div class="row g-4">
                <?php foreach ($dados['planos'] as $plano): ?>
                    <div class="col-md-6">
                        <div class="plano-card <?= $plano['chave'] === 'gratuito' ? 'plano-gratuito' : '' ?>"
                             id="plano-<?= h($plano['chave']) ?>" data-plano="<?= h($plano['chave']) ?>"
                             data-plano-nome="<?= h($plano['nome']) ?>" role="button" tabindex="0">
                            <h4><?= h($plano['nome']) ?></h4>
                            <div class="plano-preco"><?= h($plano['preco']) ?><?php if ($plano['sufixo']): ?> <span><?= h($plano['sufixo']) ?></span><?php endif; ?></div>
                            <p><?= h($plano['subtitulo']) ?></p>
                            <div class="text-start my-4">
                                <?php foreach ($plano['itens'] as $item): ?>
                                    <div class="vantagem-item">
                                        <i class="fa-solid fa-check"></i>
                                        <span><?= h($item) ?></span>
                                    </div>
                                <?php endforeach; ?>
                            </div>
                        </div>
                    </div>
                <?php endforeach; ?>
            </div>

            <input type="hidden" id="plano" name="plano" value="gratuito">

            <div class="vantagens-box mt-4">
                <h4>Por que se cadastrar?</h4>
                <?php foreach ($dados['porque'] as $p): ?>
                    <div class="vantagem-item">
                        <i class="fa-solid <?= h($p['icone']) ?>"></i>
                        <span><strong><?= h($p['titulo']) ?>:</strong> <?= h($p['texto']) ?></span>
                    </div>
                <?php endforeach; ?>
            </div>

            <div class="d-flex justify-content-between mt-4">
                <button type="button" class="btn-prev" data-passo-anterior="3"><i class="fa-solid fa-arrow-left me-2"></i> Voltar</button>
                <button type="submit" class="btn-submit"><i class="fa-solid fa-check me-2"></i> Finalizar Cadastro</button>
            </div>
        </div>
    </form>

    <!-- ===== Mensagem de sucesso (aparece no lugar do formulário) ===== -->
    <section id="cadastro-sucesso" class="form-section text-center d-none" aria-live="polite">
        <div class="sucesso-icone"><i class="fa-solid fa-circle-check"></i></div>
        <h3>Cadastro realizado com sucesso!</h3>
        <p class="mb-1">Olá <strong data-cadastro-nome></strong>,</p>
        <p class="mb-1">Seu cadastro como MEI na Vitrine MEI foi realizado com sucesso!</p>
        <p class="mb-3">Plano selecionado: <strong data-cadastro-plano></strong></p>
        <p class="text-muted">Nossa equipe irá verificar seus documentos em até 24 horas. Você receberá um e-mail assim que seu perfil estiver ativo.</p>
        <p>Obrigado por escolher a Vitrine MEI!</p>
        <a class="btn-next d-inline-block text-decoration-none" href="<?= rota('inicio') ?>">Voltar ao início</a>
    </section>

    <div class="text-center mt-5 mb-5">
        <p>Já tem uma conta? <a href="<?= rota('login') ?>">Faça login aqui</a></p>
    </div>
</div>

<?php require VIEW_PATH . '/components/layout/Rodape.php'; ?>