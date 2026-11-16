<?php
/**
 * Topo da vitrine (head + header).
 *
 * Antes de incluir, a página define:
 *   $tituloPagina     (string)  título da aba
 *   $paginaAtiva      (string)  'inicio' | 'categorias' | 'profissionais' | 'contato' | ''
 *   $descricaoPagina  (string, opcional)  meta description
 *   $estilosExtras    (array, opcional)   arquivos CSS de assets/css, ex.: ['Contato.css']
 */
require_once dirname(__DIR__, 2) . '/routes/Config.php';

$tituloPagina    = $tituloPagina ?? 'Vitrine MEI - Conectando Profissionais a Clientes';
$paginaAtiva     = $paginaAtiva ?? '';
$descricaoPagina = $descricaoPagina ?? 'Vitrine MEI - Conectando profissionais autônomos a clientes. Encontre marceneiros, eletricistas, designers, programadores e muito mais.';
$estilosExtras   = $estilosExtras ?? [];

function menu_ativo(string $pagina, string $atual): string
{
    return $pagina === $atual ? 'active' : '';
}
?>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="<?= h($descricaoPagina) ?>">
    <meta name="author" content="Vitrine MEI">

    <title><?= h($tituloPagina) ?></title>

    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600;700;800&display=swap" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css">
    <link rel="stylesheet" href="<?= asset('css/Vitrine.css') ?>">
<?php foreach ($estilosExtras as $css): ?>
    <link rel="stylesheet" href="<?= asset('css/' . $css) ?>">
<?php endforeach; ?>
</head>
<body>

<header class="header-area">
    <div class="container">
        <nav class="main-nav">
            <a href="<?= rota('inicio') ?>" class="name-site logo">Vitrine <span>MEI</span></a>

            <button type="button" class="menu-trigger" data-menu-botao aria-label="Abrir menu" aria-expanded="false">
                <i class="fa-solid fa-bars"></i>
            </button>

            <ul class="nav" data-menu>
                <li><a href="<?= rota('inicio') ?>" class="<?= menu_ativo('inicio', $paginaAtiva) ?>">Início</a></li>
                <li><a href="<?= rota('categorias') ?>" class="<?= menu_ativo('categorias', $paginaAtiva) ?>">Categorias</a></li>
                <li><a href="<?= rota('profissionais') ?>" class="<?= menu_ativo('profissionais', $paginaAtiva) ?>">Profissionais</a></li>
                <li><a href="<?= rota('contato') ?>" class="<?= menu_ativo('contato', $paginaAtiva) ?>">Contato</a></li>
                <li><a href="<?= rota('login') ?>">Login</a></li>
                <li>
                    <div class="main-white-button"><a href="<?= rota('cadastro-mei') ?>"><i class="fa-solid fa-user-plus"></i> Cadastrar MEI</a></div>
                </li>
            </ul>
        </nav>
    </div>
</header>