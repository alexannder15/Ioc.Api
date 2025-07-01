-- Create a SQL Login (server-level)
CREATE LOGIN ioc_api_user WITH PASSWORD = 'A7z!u38%KpLw91bT';

-- Map the login to the ioc database
USE ioc;
CREATE USER ioc_api_user FOR LOGIN ioc_api_user;

-- Grant necessary roles (you can fine-tune this)
ALTER ROLE db_datareader ADD MEMBER ioc_api_user;
ALTER ROLE db_datawriter ADD MEMBER ioc_api_user;

-- Optional: If your app does migrations or schema changes:
-- ALTER ROLE db_ddladmin ADD MEMBER ioc_api_user;
