// declare modal
let modal = document.getElementById("popup1");

// add method in string prototype
String.prototype.replaceAt = function (index, replacement) {
    return this.substr(0, index) + replacement + this.substr(index + replacement.length);
};
var SnakeLadderModule = (function () {
    var snakes = {
        16: 6,
        46: 25,
        49: 11,
        62: 19,
        64: 60,
        74: 53,
        89: 68,
        92: 88,
        95: 75,
        99: 80
    };
    var ladders = {
        2: 38,
        7: 14,
        8: 31,
        15: 26,
        21: 42,
        28: 84,
        36: 44,
        51: 67,
        71: 91,
        78: 98,
        87: 94
    };

    var players = [{
        name: 'Player1',
        position: 0,
        bg: '#f55bf5'
    }, {
        name: 'Player2',
        position: 0,
        bg: '#75ff79'
    },];
    var idOfplayerTurn = 0;

    /// Create snake-ladder board
    function initUI() {
        for (var i = 0; i < 10; i++) {
            var decrow = $('<div class="row"></div>');
            for (var j = 0; j < 10; j++) {
                var disVal = 0;
                if (i % 2 === 0) {
                    disVal = 10 * i + j + 1;
                } else {
                    disVal = 10 * i + 10 - j;
                }
                disVal = 100 - disVal + 1;
                decrow.append('<div id="cell_' + disVal + '"></div>'); //' + disVal + '
            }
            $('#snakenladderBoard').append(decrow[0].outerHTML);
        }
    }

    /// Build Players 
    function BuildPlayers() {
        for (var i = 0; i < players.length; i++) {
            var player = players[i];
            var playerHtml = "<span id='player" + i + "' class='player' style='background-color:" + player.bg + "'></span>";
            var playerLegendHtml = "<div id='playerLegend" + i + "' class='legends' style='background-color:" + player.bg + "'>" + player.name + "<span>0</span></div>";
            $("#players").append(playerHtml + playerLegendHtml);
        }
    }

    function RandomizeDice() {
        return Math.floor(Math.random() * 6) + 1;
    }

    return {
        Init: function () {
            initUI();
            BuildPlayers();
            $("#playerLegend" +idOfplayerTurn).addClass('active');
        },
        RollDice: function () {
            var randm = RandomizeDice();
            var die1 = document.getElementById("dice");
            die1.innerHTML = randm;

            idOfplayerTurn = idOfplayerTurn % players.length;
            $(".legends").removeClass('active');
            $("#playerLegend" + (idOfplayerTurn + 1) % players.length).addClass('active');

            var currentPosition = players[idOfplayerTurn].position;
            currentPosition += randm;
            if (currentPosition >= 100) {
                currentPosition = 100;
                modal.classList.add("show");
                $("#player" + idOfplayerTurn).appendTo("#cell_" + currentPosition);
                //$("#gameMessage").text(players[idOfplayerTurn].name + " is Winner!!!");
                $("#playerLegend" + idOfplayerTurn).find('span').text('Winner'); //currentPosition
                $("button").attr('disabled', 'disabled');
                document.getElementById("playerLegend").innerHTML = players[idOfplayerTurn].name + " is the winner!!";
                closeModal();
            }
            else {
                if(currentPosition === 2) {
                    currentPosition = 38;
                } else if (currentPosition === 7) {
                    currentPosition = 14;
                } else if (currentPosition === 8) {
                    currentPosition = 31;
                } else if (currentPosition === 15) {
                    currentPosition = 26;
                } else if (currentPosition === 21) {
                    currentPosition = 42;
                } else if (currentPosition === 28) {
                    currentPosition = 84;
                } else if (currentPosition === 36) {
                    currentPosition = 44;
                } else if (currentPosition === 51) {
                    currentPosition = 67;
                } else if (currentPosition === 78) {
                    currentPosition = 98;
                } else if (currentPosition === 71) {
                    currentPosition = 91;
                } else if (currentPosition === 87) {
                    currentPosition = 94;
                } else if (currentPosition === 99) {
                    currentPosition = 80;
                } else if (currentPosition === 95) {
                    currentPosition = 75;
                } else if (currentPosition === 92) {
                    currentPosition = 88;
                } else if (currentPosition === 89) {
                    currentPosition = 68;
                } else if (currentPosition === 74) {
                    currentPosition = 53;
                } else if (currentPosition === 62) {
                    currentPosition = 19;
                } else if (currentPosition === 64) {
                    currentPosition = 60;
                } else if (currentPosition === 46) {
                    currentPosition = 25;
                } else if (currentPosition === 49) {
                    currentPosition = 11;
                } else if (currentPosition === 16) {
                    currentPosition = 6;
                }
                
                $("#player" + idOfplayerTurn).appendTo("#cell_" + currentPosition);
                $("#gameMessage").text(players[idOfplayerTurn].name + " played " + randm);
                $("#playerLegend" + idOfplayerTurn).find('span').text(currentPosition); //currentPosition  

            }
            players[idOfplayerTurn].position = currentPosition;
            idOfplayerTurn++;
        }
    };
})();

function closeModal() {
    closeicon.addEventListener("click", function (e) {
        modal.classList.remove("show");
    });
}
SnakeLadderModule.Init();

$("button").on("click", function () {
    SnakeLadderModule.RollDice();
});
