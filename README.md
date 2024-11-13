https://docs.google.com/document/d/1pQ6whUMU-qRZBEJi2MilWi8ZapFWuD8kSKRJnn9XmR8/edit?u
## Commandes pour Commencer

### Créer une Nouvelle Application Web
```bash
dotnet new mvc -n NomDuProjet
```

### Nettoyer Fichier de Build
```bash
dotnet dotnet clean -- dotnet build
```

### Exécuter l'Application
```bash
dotnet run
```

### Surveiller les Changements et Exécuter
```bash
dotnet watch run
```

### Faire Confiance au Certificat HTTPS
```bash
dotnet dev-certs https --trust
```

## Gestion des Outils

### Désinstaller Générateur de Code ASP.NET
```bash
dotnet tool uninstall --global dotnet-aspnet-codegenerator
```

### Installer le Générateur de Code ASP.NET
```bash
dotnet tool install --global dotnet-aspnet-codegenerator
```

### Désinstaller  les Outils Entity Framework Core
```bash
dotnet tool uninstall --global dotnet-ef
```

### Installer les Outils Entity Framework Core
```bash
dotnet tool install --global dotnet-ef
```

## Ajouter les Packages Nécessaires

### Conception Entity Framework Core
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### Fournisseur SQLite pour Entity Framework Core
```bash
dotnet add package Microsoft.EntityFrameworkCore.SQLite
```

### Conception de Génération de Code Web Visual Studio
```bash
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

### Fournisseur SQL Server pour Entity Framework Core
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

### Outils Entity Framework Core
```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

## Générer des Pages Razor

### Générer un Crud Total pour le Modèle `Film`
```bash
dotnet aspnet-codegenerator razorpage -m Movie -dc RazorPagesMovie.Data.RazorPagesMovieContext -udl -outDir Pages/Movies --referenceScriptLibraries --databaseProvider sqlite
```

### Utilisez l’option -h pour obtenir de l’aide sur la commande
```bash
dotnet aspnet-codegenerator razorpage -h
```

## Migrations

### Génère le code nécessaire à la création du schéma de base de données initial. Le schéma est basé sur le modèle spécifié dans DbContext.
```bash
dotnet ef migrations add InitialCreate
```

### La commande update exécute la méthode Up dans des migrations qui n’ont pas été appliquées 
```bash 
dotnet ef database update -- Atteantion pas de "dotnet watch run" sinon conflit
```