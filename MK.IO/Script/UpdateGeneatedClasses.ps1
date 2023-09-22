# Define the path to the .cs files
$filePath = "C:\\Repos\\MK.IO\\MK.IO\\CsharpDotNet2\\Model - Copy\\*.cs"

# Get the files from the folder and iterate using Foreach
Get-ChildItem $filePath -Recurse | ForEach-Object {
    # Read the file content
    $content = Get-Content $_.FullName

    # Replace the old string with the new string
    $content = $content -replace 'namespace IO.Swagger.Model', 'namespace MK.IO.Models'

    # Write the new content back to the file
    Set-Content -Path $_.FullName -Value $content
}
