$(document).ready(function () {
    let checkMenu = false;
    let checkItem = false;
    $('.openbtn').on('click', function () {
        $('#mySidebar').toggleClass('sidbar-toggle');
        $('.text-collapse ').toggleClass('hide-text');
        $('#main').toggleClass('main-toggle');
        $('.dropdown__menu-list').removeClass('dropdown-display');
        
        
    });
       


    let min = $('#acount').attr('min');
    let max = $('#acount').attr('max');
    let amount = $('#acount').val();
    $('#desc').on('click', function(){
        if(amount === min) {
            l
        }
    });


    $("[type='radio']").on('click', function (e) {
        var previousValue = $(this).attr('previousValue');
        if (previousValue == 'true') {
            this.checked = false;
            $(this).attr('previousValue', this.checked);
        }
        else {
            this.checked = true;
            $(this).attr('previousValue', this.checked);
        }
    });
    var i = 1;

    $('.fa-plus').on('click',
        function() {
            i += 1;
            var table = document.getElementsByClassName('table-light')[0];
            if (!table) return;
            var rowLength = table.rows.length;

            var newRow = document.createElement('tr'); // create row node
            //var col = table.rows[1].cells[0].cloneNode(true); // create column node
            console.log(newRow);
            var col = document.createElement('td');
            var col1 = document.createElement('td');
            var col2 = document.createElement('td');


            newRow.appendChild(col);
            newRow.appendChild(col1); // append second column to row
            newRow.appendChild(col2);
            var medName = "txtMedName" + i;
            var medType = "txtMedType" + i;
            var medUsage = "txtMedUsage" + i;
            col.innerHTML +=
            `<input class="form-control" type="text" name="${medName}" placeholder="İlaç" style="text-align: center;">`; // put data in first column
            col1.innerHTML +=
                `<input class="form-control" type="text" name="${medType}" placeholder="Türü" style="text-align: center;">`; // put data in second column
            col2.innerHTML +=
                `<input class="form-control" type="text" name="${medUsage}" placeholder="Kullanım" style="text-align: center;">`;

            var tBody = document.getElementsByClassName('tbody')[0];
            tBody.insertBefore(newRow, tBody.lastChild);

        });

});
