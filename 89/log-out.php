<?php
// Démarrer la session
session_start();

// Vérifier si l'utilisateur est connecté
if(isset($_SESSION['username'])) {
    // Si l'utilisateur est connecté, détruire la session
    session_destroy();
    
    // Rediriger l'utilisateur vers la page de connexion par exemple
    header("Location: pages-login.php");
    exit; // Assurez-vous de quitter le script après la redirection
} else {
    // Si l'utilisateur n'est pas connecté, afficher un message d'erreur ou rediriger vers une autre page
    echo "Vous n'êtes pas connecté.";
}
?>
