function JSONSerializationDialog() {
    if (confirm("Serialize this page to JSON? "))
        return true;
    else
        return false;
}
function XMLSerializationDialog() {
    if (confirm("Serialize this page to XML? "))
        return true;
    else
        return false;
}
function JSONDeserializationDialog() {
    if (confirm("Deserialize JSON version of this page? "))
        return true;
    else
        return false;
}
function XMLDeserializationDialog() {
    if (confirm("Deserialize XML version of this page? "))
        return true;
    else
        return false;
}