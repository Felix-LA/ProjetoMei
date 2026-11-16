<?php
/**
 * Edita o nome de um usuário já cadastrado.
 * A API exige todos os campos no PUT, então buscamos o cadastro atual (GET /id),
 * trocamos o nome e reenviamos tudo. Novos usuários entram pelo CadastroMei.php.
 */
require_once __DIR__ . '/../../routes/Config.php';

if ($_SERVER['REQUEST_METHOD'] !== 'POST' || !csrf_validar()) {
    flash('erro', 'Requisição inválida.');
    redirecionar('usuarios');
}

$id   = (int) ($_POST['id'] ?? 0);
$nome = trim($_POST['nome'] ?? '');

if ($id <= 0) {
    flash('erro', 'Para cadastrar um novo MEI, use a página de cadastro.');
    redirecionar('usuarios');
}

if ($nome === '' || mb_strlen($nome) > 100) {
    flash('erro', 'Informe um nome com até 100 caracteres.');
    redirecionar('usuarios');
}

$atual = api('GET', "/$id");
if ($atual['erro'] !== null || !is_array($atual['dados'])) {
    flash('erro', $atual['status'] === 404 ? 'Usuário não encontrado.' : api_mensagem($atual));
    redirecionar('usuarios');
}

$corpo = $atual['dados'];
$corpo['nome'] = $nome;

$resp = api('PUT', "/$id", $corpo);

if ($resp['erro'] === null) {
    flash('sucesso', 'Usuário atualizado com sucesso.');
} else {
    flash('erro', api_mensagem($resp));
}

redirecionar('usuarios');