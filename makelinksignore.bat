@echo off
cd ChessSonification
mklink /d "Libraries" "..\Libraries"
cd ..
cd NumbersSonification
mklink /d "Libraries" "..\Libraries"

echo.
echo.
echo You should see the symbolic links created above with the weird arrows. 
echo If not, the links might already be created!
echo.
echo Type 'exit' to close this
