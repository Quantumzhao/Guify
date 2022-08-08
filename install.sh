if [ ! -d "$HOME/.local/share" ]; then
    mkdir $HOME/.local/share
fi

chmod +x ./Guify

cp `pwd` $HOME/.local/share/guify

ln -s ./Guify guify
mv ./guify $HOME/.local/bin/guify

echo "done."

