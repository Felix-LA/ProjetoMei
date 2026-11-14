<?php
/** Exclui um usuário na API (DELETE). */
require_once __DIR__ . '/../../routes/config.php';

if ($_SERVER['REQUEST_METHOD'] !== 'POST' || !csrf_validar()) {
    flash('erro', 'Requisição inválida.');
    redirecionar('usuarios');
}

$id = (int) ($_POST['id'] ?? 0);

if ($id <= 0) {
    flash('erro', 'Usuário inválido.');
    redirecionar('usuarios');
}

$resp = api('DELETE', "/$id");

if ($resp['erro'] === null) {
    flash('sucesso', 'Usuário excluído com sucesso.');
} else {
    flash('erro', $resp['erro']);
}

redirecionar('usuarios');