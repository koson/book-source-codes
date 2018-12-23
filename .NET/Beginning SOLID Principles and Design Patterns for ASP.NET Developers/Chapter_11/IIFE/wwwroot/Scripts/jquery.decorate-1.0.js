/// <reference path="jquery-2.1.1.js" />

(function ($) {
    $.fn.decorate = function (settings) {
        var defaultSettings = {};
        var finalSettings = $.extend({}, defaultSettings, settings);
        this.each(function () {
            $(this).css("font-size", finalSettings.fontSize)
                   .css("color", finalSettings.foreColor)
                   .css("background-color", finalSettings.backColor);
        });
    }
})(jQuery);