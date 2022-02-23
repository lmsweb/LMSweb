let inputChList = new Array();
let deleteChList = new Array();
let inputString = "";
let deleteString = "";
editor.on('change', (ins, ch) => {

    let actionType = "";
    let index = Number(ch.from.ch.toString());
    let lineNumbers = ins.getCursor().line;
    inputChList = Array.from(editor.getLine(lineNumbers).toString());

    if (ch.origin == "+input") {
        // inputChList = Array.from(editor.getLine(lineNumbers).toString());
        // console.log("inputChList.length " + inputChList.length + " index " + index);

        actionType = "新增";
        if (ch.text.length > 1 && inputChList.length <= index)    //字尾換行
        {
            console.log("動作：" + actionType + " 行號：" + lineNumbers + " 內容：" + inputString);

            clearTmp();
        }
        else if (ch.text.length > 1 && inputChList.length > index)    //非字尾換行
        {
            let sub = inputString.substring(0, index);
            let sub2 = inputString.substring(index);
            console.log("動作：" + actionType + " 行號：" + lineNumbers + " 內容：" + sub);
            console.log("動作：" + actionType + " 行號：" + (lineNumbers + 1) + " 內容：" + sub2);
            clearTmp();
        }

        // else if((ch.from.sticky != null) && (ch.from.sticky.toString().toLowerCase() == "before") && (ch.from.xRel != null))   //修改該行內容
        // {
        //     console.log("動作：" + "修改" + " 行號：" + (lineNumbers+1) + " 內容：" + inputString);
        //     clearTmp();
        // }

        else    //輸入一個字元
        {
            if (ch.text[0] != "" && ch.text[0] != null) {
                // inputChList.splice(index, 0, ch.text[0]);
            }

        }

    }
    else if (ch.origin == "+delete") {
        if ((ch.from.sticky != null) && (ch.from.sticky.toString() == "1"))    //刪除一個字元
        {
            // inputChList.splice(index, 1);
            deleteChList.push(ch.removed[0]);
        }
        else if ((ch.from.sticky != null) && (ch.from.sticky.toString() == "before") && (ch.removed.length > 1))    //刪除一行
        {
            deleteString = deleteChList.reverse().join('');

            console.log("動作：刪除 行號：" + (lineNumbers + 2) + " 內容：" + deleteString);
            clearTmp();
        }

    }

    inputString = inputChList.join('');
    deleteString = deleteChList.join('');
    //console.log(inputString);


});

function clearTmp() {
    inputString = "";
    inputChList.length = 0;
    deleteString = "";
    deleteChList.length = 0;
}