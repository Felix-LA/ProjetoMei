<?php
/** Recebe o formulário do modal e cria (POST) ou atualiza (PUT) o usuário na API. */
require_once __DIR__ . '/../../routes/config.php';

if ($_SERVER['REQUEST_METHOD'] !== 'POST' || !csrf_validar()) {
    flash('erro', 'Requisição inválida.');
    redirecionar('usuarios');
}

$id   = (int) ($_POST['id'] ?? 0);
$nome = trim($_POST['nome'] ?? '');

if ($nome === '' || mb_strlen($nome) > 50) {
    flash('erro', 'Informe um nome com até 50 caracteres.');
    redirecionar('usuarios');
}

if ($id > 0) {
    $resp = api('PUT', "/$id", ['id' => $id, 'nome' => $nome]);
    $ok   = 'Usuário atualizado com sucesso.';
} else {
    $resp = api('POST', '', ['nome' => $nome]);
    $ok   = 'Usuário cadastrado com sucesso.';
}

if ($resp['erro'] === null) {
    flash('sucesso', $ok);
} else {
    flash('erro', $resp['erro']);
}

redirecionar('usuarios');