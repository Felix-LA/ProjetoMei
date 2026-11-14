<?php
/**
 * Recebe o formulário de login.
 * ATENÇÃO: a API ainda não tem autenticação, então aqui só validamos os campos.
 * Quando a API tiver o endpoint de login, chame api('POST', ...) neste ponto.
 */
require_once __DIR__ . '/../routes/config.php';

if ($_SERVER['REQUEST_METHOD'] !== 'POST' || !csrf_validar()) {
    flash('erro', 'Requisição inválida.');
    redirecionar('login');
}

$email = trim($_POST['email'] ?? '');
$senha = $_POST['senha'] ?? '';

if (!filter_var($email, FILTER_VALIDATE_EMAIL) || $senha === '') {
    flash('erro', 'Informe e-mail e senha válidos.');
    redirecionar('login');
}

// TODO: autenticar na API quando o endpoint existir.
flash('erro', 'O login ainda não está disponível: a API não possui autenticação.');
redirecionar('login');