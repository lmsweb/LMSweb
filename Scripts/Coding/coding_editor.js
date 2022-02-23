//根據DOM元素的id構造出一個編輯器
const editor = CodeMirror.fromTextArea(document.getElementById("code"), {
    mode: {
        name: "python",
        version: 3,
        singleLineStringErrors: false
    },
    lineNumbers: true,
    theme: "seti",
    fullScreen: true,    //全屏模式
    keyMap: "vim",    //綁定Vim
});

let online = $.connection.codeServiceHub;
online.client.hello = function () {
    $('.CodeMirror').text( "print('Hello')");
}

$.connection.hub.start().done(function () {
    online.server.hello();
});