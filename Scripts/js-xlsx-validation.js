function write_ws_xml_datavalidation(validations) {
    var o = '<dataValidations>';
    for (var i = 0; i < validations.length; i++) {
        var validation = validations[i];
        o += '<dataValidation type="list" allowBlank="1" sqref="' + validation.sqref + '">';
        o += '<formula1>&quot;' + validation.values + '&quot;</formula1>';
        o += '</dataValidation>';
    }
    o += '</dataValidations>';
    return o;
}