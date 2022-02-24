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
//online.client.hello = function () {
//    editor.setValue("print(Hello);");
//}

$.connection.hub.start().done(function () {

});


let oldContent = "";
let newContent = "";
let cursor = { line : 0, ch : 0};

editor.on('change', (ins, ch) => {

    //console.log(editor.getCursor());

    newContent = editor.getValue();

    if (oldContent != newContent) {
        cursor.line = editor.getCursor().line;
        cursor.ch = editor.getCursor().ch;

        online.server.editCode("name", editor.getValue());
        oldContent = newContent;
        
        console.log(cursor);
        editor.setCursor(cursor);
    }
    

});


online.client.broadcastCode = function (name, content) {

    editor.setValue(content);

}