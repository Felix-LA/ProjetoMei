<?php
/**
 * Cabeçalho comum. Antes de incluir, a página define:
 *   $tituloPagina (string)  e  $paginaAtiva ('inicio' | 'usuarios' | 'login')
 *   $scriptsExtras (array opcional) com nomes de arquivos JS em assets/js
 */
require_once dirname(__DIR__, 2) . '/routes/config.php';

$tituloPagina = $tituloPagina ?? 'Projeto MEI';
$paginaAtiva  = $paginaAtiva ?? '';
$flash        = flash_pegar();
?>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?= h($tituloPagina) ?> · Projeto MEI</title>
    <link rel="stylesheet" href="<?= assets('styles/style.css') ?>">
</head>
<body>

<header class="topo">
    <div class="container topo__conteudo">
        <a class="topo__marca" href="<?= Rotas('inicio') ?>">Projeto <strong>MEI</strong></a>
        <nav class="topo__menu">
            <a href="<?= Rotas('inicio') ?>" class="<?= $paginaAtiva === 'inicio' ? 'ativo' : '' ?>">Início</a>
            <a href="<?= Rotas('inicio') ?>#vitrine">Vitrine</a>
            <a href="<?= Rotas('usuarios') ?>" class="<?= $paginaAtiva === 'usuarios' ? 'ativo' : '' ?>">Usuários</a>
            <a href="<?= Rotas('login') ?>" class="btn btn--pequeno topo__login">Login</a>
        </nav>
    </div>
</header>

<main class="container conteudo">
<?php if ($flash): ?>
    <div class="alerta alerta--<?= h($flash['tipo']) ?>" data-alerta role="alert">
        <span><?= h($flash['mensagem']) ?></span>
        <button type="button" class="alerta__fechar" data-alerta-fechar aria-label="Fechar">&times;</button>
    </div>
<?php endif; ?>