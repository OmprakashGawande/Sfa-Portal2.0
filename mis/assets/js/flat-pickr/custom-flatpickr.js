(function () {
    // 1. Default Date
    flatpickr(".datetime-local", {
        dateFormat: "d/m/Y"
    });

    // 2. Human Friendly
    flatpickr(".human-friendly", {
        altInput: true,
        altFormat: "F j, Y",
        dateFormat: "d/m/Y"
    });

    // 3. min-max value
    flatpickr(".min-max", {
        dateFormat: "d/m/Y",
        maxDate: "15/12/2017"
    });

    // 4. disabled-date
    flatpickr(".disabled-date", {
        disable: ["30/01/2025", "21/02/2025", "08/03/2025", new Date(2025, 4, 9)],
        dateFormat: "d/m/Y"
    });

    // 5. multiple-date
    flatpickr(".multiple-date", {
        mode: "multiple",
        dateFormat: "d/m/Y"
    });

    // 6. Customizing the Conjunction
    flatpickr(".customize-date", {
        mode: "multiple",
        dateFormat: "d/m/Y",
        conjunction: " :: "
    });

    // 7. Range-date
    flatpickr(".range-date", {
        mode: "range",
        dateFormat: "d/m/Y"
    });

    // 8. Disabled Range
    flatpickr(".preloading-date", {
        mode: "multiple",
        dateFormat: "d/m/Y",
        defaultDate: ["20/10/2016", "04/11/2016"]
    });

    // 9. Time-picker
    flatpickr(".time-picker", {
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i"
    });

    // 10. 24-hour Time Picker
    flatpickr(".twenty-four-hour", {
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
        time_24hr: true
    });

    // 11. Time Picker W/Limits
    flatpickr(".limit-time", {
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
        minTime: "16:00",
        maxTime: "22:30"
    });

    // 12. Preloading Time
    flatpickr(".preloading-time", {
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
        defaultDate: "13:45"
    });

    // 13. DateTimePicker with Limited Time Range[min-time]
    flatpickr(".limit-time-range", {
        enableTime: true,
        minTime: "09:00",
        dateFormat: "d/m/Y"
    });

    // 14. DateTimePicker with Limited Time Range[min/max-time]
    flatpickr(".limit-min-max-range", {
        enableTime: true,
        minTime: "16:00",
        maxTime: "22:00",
        dateFormat: "d/m/Y"
    });

    // 15. Date With Time
    flatpickr(".datetime-local1", {
        enableTime: true,
        dateFormat: "d/m/Y H:i"
    });

    // 16. Inline Calender
    flatpickr(".inline-calender", {
        inline: true,
        dateFormat: "d/m/Y"
    });
})();
