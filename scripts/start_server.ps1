export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  #This loads mvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"  
cd /var/www/html/lit-test/build_output
pm2 start "dotnet ChuckSwapiCAssessment.API.dll" --name lit-test