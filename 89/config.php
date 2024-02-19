
 <?php
$serveur = "localhost";
$utilisateur = "root";
// $mot_de_passe = "il123all456um789DG852";
$mot_de_passe = "";
$nom_base_de_donnees = "startup";

// Connexion à la base de données
$connexion = new mysqli($serveur, $utilisateur, $mot_de_passe, $nom_base_de_donnees);
$connexion->set_charset("utf8");
// Vérification de la connexion
if ($connexion->connect_error) {
    die("Échec de la connexion à la base de données : " . $connexion->connect_error);
}
?>