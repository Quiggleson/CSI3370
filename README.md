<h2>Download <a href="https://dotnet.microsoft.com/en-us/download/dotnet/7.0">.NET SDK 7.0</a></h2>


<h2>Configure AWS credentials</h2>
Download <a href="https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html">AWS CLI</a><br><br>

Test the download by running ``aws --version`` in a terminal. May require relaunching a terminal instance.

Run ``aws configure --profile "s3profile"``

You will be prompted for an access key, a secret key, and a default region

For the access keys, see the document in the root directory of this project. Note: the user in the accesskeys file is the username of the IAM user, this is not "s3profile". This is on purpose. You don't really need the username of the IAM user, but it's there in case we might, so disregard it for now.

For the default region, enter ``us-east-2``

Confirm the credentials are right by opening ``config`` and ``credentials`` in the ``Users/{user}/.aws/`` directory. ``config`` should have the region and ``credentials`` should have the access keys

<h2>Running the goods </h2>
Run from Visual Studio or

Run ``dotnet run`` in the command line

Go to /Accounts to see the CRUD pages

Go to /database to test the AWS RDS MySQL connection

Go to /tests3 to test the s3 bucket
