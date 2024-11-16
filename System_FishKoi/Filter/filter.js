const dateFormat = () => {
    return function (input, format) {

        if (isNullOrEmpty(input)) return input;

        const milliseconds = parseInt(input.replace(/\/Date\((-?\d+)\)\//, '$1'));

        // Default format if not provided
        format = format || 'dd/MM/yyyy';
        const date = new Date(milliseconds);


        // Format the date using provided format
        var year = date.getFullYear();
        var month = ('0' + (date.getMonth() + 1)).slice(-2);
        var day = ('0' + date.getDate()).slice(-2);

        // Replace format placeholders with actual values
        format = format.replace('yyyy', year);
        format = format.replace('MM', month);
        format = format.replace('dd', day);

        return format;
    }
}


const cDate = () => {
    return function (input) {
        if (isNullOrEmpty(input)) return input;
        const milliseconds = parseInt(input.replace(/\/Date\((-?\d+)\)\//, '$1'));
        const date = new Date(milliseconds);

        return date;
    }
}