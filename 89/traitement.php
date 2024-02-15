<?php
include('config.php');

// Récupération des données du formulaire
$nic = $_POST['nic'];
$email = $_POST['email'];
$phone = $_POST['phone'];
$username = $_POST['username'];
$password = $_POST['password'];

// Vérification si le NIC existe dans la base de données
$sql = "SELECT * FROM enseignant WHERE nic = '$nic'";
$result = $connexion->query($sql);

if ($result->num_rows > 0) {
    // Le NIC existe dans la base de données, effectuer l'insertion des données
    $sql_insert = "UPDATE enseignant SET email = '$email', tele = '$phone', username = '$username', password = '$password' WHERE nic = '$nic'";
    
    if ($connexion->query($sql_insert) === TRUE) {
		$sql_nom_prenom = "SELECT lastnamefr, firstnamefr FROM enseignant WHERE nic = '$nic'";
        $result_nom_prenom = $connexion->query($sql_nom_prenom);
        $row_nom_prenom = $result_nom_prenom->fetch_assoc();
        $nom = $row_nom_prenom['lastnamefr'];
        $prenom = $row_nom_prenom['firstnamefr'];

		
		
		
       echo "Bienvenue $nom $prenom, votre enregistrement a été mis à jour avec succès vous pouvez connecter dans la plateforme avec votre user et password ";
 
	
	} else {
        echo "Erreur lors de la mise à jour de l'enregistrement : " . $connexion->error;
    }
} else {
    echo "Le NIC n'existe pas dans la base de données";
}

// Fermeture de la connexionexion à la base de données
$connexion->close();
?>
