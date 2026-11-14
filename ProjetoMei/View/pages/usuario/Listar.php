<?php
/** Usuários: listagem, busca por nome e acesso aos modais de cadastro/edição/exclusão. */
$tituloPagina  = 'Usuários';
$paginaAtiva   = 'usuarios';
$scriptsExtras = ['usuario.js'];
require_once __DIR__ . '/../../routes/config.php';
require VIEW_PATH . '/components/layout/header.php';

$busca    = trim($_GET['nome'] ?? '');
$usuarios = [];
$erroApi  = null;

if ($busca !== '') {
    $resp = api('GET', '/nome/' . rawurlencode($busca));
} else {
    $resp = api('GET');
}

if ($resp['status'] === 404) {
    $usuarios = []; // nada encontrado
} elseif ($resp['erro'] !== null && $resp['status'] !== 500) {
    $erroApi = $resp['erro'];
} elseif ($resp['status'] === 500 && $busca !== '') {
    $usuarios = []; // API atual lança Exception quando não acha (vira 500)
} elseif ($resp['status'] === 500) {
    $erroApi = $resp['erro'];
} else {
    $dados = $resp['dados'];
    if (is_array($dados) && isset($dados['id'])) {
        $dados = [$dados]; // a busca por nome pode devolver um único objeto
    }
    $usuarios = is_array($dados) ? $dados : [];
}
?>

<div class="barra">
    <h1>Usuários</h1>
    <button type="button" class="btn" data-modal-abrir="modal-usuario" data-acao="novo">+ Novo usuário</button>
</div>

<form method="get" class="busca">
    <input type="search" name="nome" value="<?= h($busca) ?>" placeholder="Buscar por nome...">
    <button type="submit" class="btn btn--claro">Buscar</button>
    <?php if ($busca !== ''): ?>
        <a class="btn btn--link" href="<?= rota('usuarios') ?>">Limpar</a>
    <?php endif; ?>
</form>

<?php if ($erroApi): ?>
    <div class="alerta alerta--erro"><span><?= h($erroApi) ?></span></div>
<?php endif; ?>

<div class="tabela-wrap">
    <table class="tabela">
        <thead>
            <tr>
                <th style="width: 90px">ID</th>
                <th>Nome</th>
                <th style="width: 190px">Ações</th>
            </tr>
        </thead>
        <tbody>
        <?php if (!$usuarios): ?>
            <tr><td colspan="3" class="tabela__vazio">Nenhum usuário encontrado.</td></tr>
        <?php else: foreach ($usuarios as $u): ?>
            <tr>
                <td><?= h($u['id'] ?? '') ?></td>
                <td><?= h($u['nome'] ?? '') ?></td>
                <td class="tabela__acoes">
                    <button type="button" class="btn btn--pequeno btn--claro"
                            data-modal-abrir="modal-usuario" data-acao="editar"
                            data-id="<?= h($u['id'] ?? '') ?>" data-nome="<?= h($u['nome'] ?? '') ?>">Editar</button>
                    <button type="button" class="btn btn--pequeno btn--perigo"
                            data-modal-abrir="modal-confirmar" data-acao="excluir"
                            data-id="<?= h($u['id'] ?? '') ?>" data-nome="<?= h($u['nome'] ?? '') ?>">Excluir</button>
                </td>
            </tr>
        <?php endforeach; endif; ?>
        </tbody>
    </table>
</div>

<?php
require VIEW_PATH . '/components/modais/modal-usuario.php';
require VIEW_PATH . '/components/modais/modal-confirmar.php';
require VIEW_PATH . '/components/layout/footer.php';
?>