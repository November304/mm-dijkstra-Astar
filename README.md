# README : Algorithmes de Dijkstra et A\*

## Introduction

Ce projet explore les algorithmes de **Dijkstra** et **A\***, deux méthodes clés pour la recherche de chemins optimaux dans des graphes pondérés. En partant de Dijkstra, étudié en R2.07, nous approfondissons avec l'algorithme A\*, une amélioration qui intègre une heuristique pour une recherche plus rapide dans certaines situations.

## Objectifs

- **Comprendre l'algorithme de Dijkstra** : Analyser ses bases et identifier ses limites pour des cas pratiques.
- **Découvrir l'algorithme A\*** : Voir comment l'ajout d'une heuristique permet de réduire le nombre de nœuds à explorer et d'optimiser le temps de recherche.
- **Étudier des applications concrètes** dans différents domaines :
  - **Navigation GPS et applications de cartographie** (Waze, Google Maps) : Calculs d'itinéraires optimisés en temps réel.
  - **Jeux vidéo** : A\* est utilisé pour la navigation de personnages dans des environnements complexes avec obstacles.
  - **Réseaux informatiques** : Pour optimiser la transmission des données en trouvant les routes les plus efficaces.

## Modélisation avec Unity

Pour visualiser ces algorithmes, nous utilisons **Unity** afin de modéliser des exemples interactifs. Cette modélisation permet d'illustrer le comportement des algorithmes en montrant le résultat direct, ou en suivant les étapes de calcul. Actuellement le graphe representé est un graphe de degré 4, avec un mouvement possible vers les 4 directions cardinales, sauf sur les cases noires qui representent un obstacle.

Pour lancer la scène Unity, dezipper le fichier DemoLinux.zip sur linux et executer le fichier 'DemoUnity.x86_64', sur Windows, dezipper le fichier DemoWindows.zip et executer le fichier 'DemoUnity.exe'.

Vous pouvez alors générer un graphe type labyrinthe en appuyant sur le bouton Generate, effacer les obstacles en appuyant dessus avec un clic gauche, ou en replacer avec un clic droit. Vous pouvez ensuite choisir une position de départ et une position d'arrivée en appuyant sur les boutons Set manually et en cliquant sur une case du graphe. Enfin, vous pouvez choisir l'algorithme a tester entre Dijkstra et A\* choissiant l'option dans les dropdowns, puis choisir entre deroulement instantané (ALL_IN_ONE) ou pas à pas (STEP_BY_STEP) en cliquant sur le bouton correspondant. Vous pouvez alors lancer l'algorithme en appuyant sur le bouton Generate Path.

La case verte represente la position de départ, la case bleue la position d'arrivée, les cases jaune le chemin trouvé par l'algorithme, les cases noires les obstacles, les cases rouges les chemins explorés par l'algorithme et les cases blanches les cases non explorées.

Dans le mode pas à pas, le nombre au milieu de la cellule represente la somme de la distance à la cellule de départ et de la valeur de l'heuristique (uniquement dans le cas de A*\), le nombre en haut à gauche la distance à la cellule de départ, et le nombre en bas à droite la valeur de l'heuristique. 

## Structure du projet

Ce projet comprend :
- **Documentation et Implémentations de Dijkstra et A\*** en Python et Markdown dans un Jupyter Notebook,
- **Cas pratiques** 3 jupyter notebooks dans le dossier CasPratique, pour parler du cas de la navigation, des jeux vidéos et des réseaux informatiques.
- **Ressources Unity** pour les ressources Unity, et tout le projet que l'on peut lancer avec Unity6
- **Build Unity** pour visualiser le projet directement, deux executables pour linux et windows, zippés dans les fichiers DemoLinux.zip et DemoWindows.zip.
