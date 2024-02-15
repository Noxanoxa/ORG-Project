<?php
// Démarrer la session
session_start();

// Inclure le fichier de configuration de la base de données
require_once 'config.php';

// Récupérer les données du formulaire
$username = $_POST['username'];
$password = $_POST['password'];

// Effectuer une requête SQL pour vérifier les informations d'identification
$query = "SELECT * FROM enseignant WHERE username = '$username' AND password = '$password'";
$result = mysqli_query($connexion, $query);

// Vérifier si une correspondance est trouvée
if(mysqli_num_rows($result) == 1) {
    // Authentification réussie
    $_SESSION['username'] = $username; // Enregistrer le nom d'utilisateur dans la session
   
   // Vous pouvez également stocker d'autres informations de l'utilisateur dans la session si nécessaire
    
    echo 'success'; // Renvoyer une réponse de succès
} else {
    echo 'InvalidUsernamePassword'; // Identifiants invalides
}

// Fermer la connexion à la base de données
mysqli_close($connexion);
?>
