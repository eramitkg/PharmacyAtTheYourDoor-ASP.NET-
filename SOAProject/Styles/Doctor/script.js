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
    $('.fa-plus').on('click',function(){
        var table = document.getElementsByClassName('table-light')[0];
        if (!table) return;
        var rowLength = table.rows.length
        
        var newRow = document.createElement('tr'); // create row node
        //var col = table.rows[1].cells[0].cloneNode(true); // create column node
        var col  = document.createElement('td');
        var col1 = document.createElement('td');
        var col2 = document.createElement('td');
        var col3 = document.createElement('td');
        
    
        newRow.appendChild(col); // append first column to row
        newRow.appendChild(col1);
        newRow.appendChild(col2); // append second column to row
        newRow.appendChild(col3);
        col.innerHTML += `<input class="form-control" type="text" style="text-align: center;" readonly>`
        col1.innerHTML += `<input class="form-control" type="text" placeholder="İlaç" style="text-align: center;">`; // put data in first column
        col2.innerHTML += `<input class="form-control" type="number" placeholder="Adet" style="text-align: center;">`; // put data in second column
        col3.innerHTML += `
        <td>
                    <label for="using-morning${rowLength}">Sabah</label>
                    <input id=using-morning${rowLength} type="checkbox" style="text-align: center;">
                    <label for="using-afternoon${rowLength}">Öğle</label>
                    <input id=using-afternoon${rowLength} type="checkbox" style="text-align: center;">
                    <label for="using-evening${rowLength}">Akşam</label>
                    <input id=using-evening${rowLength} type="checkbox" style="text-align: center;">
                </td>
        `;
        
        var tBody = document.getElementsByClassName('tbody')[0];
        tBody.insertBefore(newRow, tBody.lastChild);
        
    })

});
