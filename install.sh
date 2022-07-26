if [ ! -d "$HOME/.local/share" ]; then
    mkdir $HOME/.local/share
fi

cp `pwd` $HOME/.local/share/guify

