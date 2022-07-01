mv . guify

if [ ! -d "$HOME/.local/share" ]; then
    mkdir $HOME/.local/share
fi

cp . $HOME/.local/share

