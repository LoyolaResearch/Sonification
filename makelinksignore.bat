@echo off

echo If prompted to delete something (..\Libraries\*), it is safe to answer Y or N.
echo This just means the symbolic links already exists correctly.
echo.

cd ChessSonification
del Libraries
mklink /d "Libraries" "..\Libraries"
cd ..
cd NumbersSonification
del Libraries
mklink /d "Libraries" "..\Libraries"

echo.
echo.
echo You should see the symbolic links created above with the weird arrows. 
echo If not, the links might already be created!
echo.
echo Type 'exit' to close this
