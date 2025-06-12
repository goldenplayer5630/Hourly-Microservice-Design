# Reset-Database.ps1
Write-Host "Dropping the existing database..."
dotnet ef database drop --force

# Step 2: Remove all existing migrations one by one
Write-Host "Removing all existing migrations..."
dotnet ef migrations remove --force

# Step 3: Ensure the Migrations folder is gone (in case anything remains)
if (Test-Path "./Migrations") {
    Write-Host "Deleting Migrations folder manually..."
    Remove-Item -Recurse -Force "./Migrations"
}

# Step 4: Create a new initial migration
Write-Host "Creating initial migration 'initCreate'..."
dotnet ef migrations add initCreate

# Step 5: Apply the migration to recreate the database
Write-Host "Applying migration to database..."
dotnet ef database update

Write-Host "Database reset and initialized successfully!"

# Step 6: Run the seeder
# Write-Host "Seeding the database..."
#  run --project ../Hourly.DatabaseSeeder/Hourly.DatabaseSeeder.csproj  # Adjust the path if needed
