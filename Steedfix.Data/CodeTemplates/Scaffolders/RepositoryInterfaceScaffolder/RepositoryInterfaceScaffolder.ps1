[T4Scaffolding.Scaffolder(Description = "Makes an EF DbContext able to persist models of a given type, creating the DbContext first if necessary")][CmdletBinding()]
param(        
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ModelType,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders
)

# Ensure we can find the model type
$foundModelType = Get-ProjectType $ModelType -Project $Project
if (!$foundModelType) { return }
	
$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$modelTypeNamespace = [T4Scaffolding.Namespaces]::GetNamespace($foundModelType.FullName)

$outputPath = "I" + $foundModelType.Name + "Repository"
$outputPath = Join-Path Interfaces $outputPath

$interfaceNamespace = [T4Scaffolding.Namespaces]::Normalize($defaultNamespace + "." + [System.IO.Path]::GetDirectoryName($outputPath).Replace([System.IO.Path]::DirectorySeparatorChar, "."))
Add-ProjectItemViaTemplate $outputPath -Template RepositoryInterfaceScaffolder -Model @{
	InterfaceNamespace = $interfaceNamespace; 
	ModelTypeNamespace = $modelTypeNamespace;
	ModelName = $foundModelType.Name;
} -SuccessMessage "Added Repository '{0}'" -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
