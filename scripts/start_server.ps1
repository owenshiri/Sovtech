export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  #This loads mvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"  
cd /var/www/html/lit-test/bin/release/netcoreapp3.1/linux-x64/publish
pm2 start "dotnet ChuckSwapiCAssessment.API.dll" --name tmro