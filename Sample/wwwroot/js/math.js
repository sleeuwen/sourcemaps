(function () {
    const incrementImpl = (val) => { window.undefinedFunctionCall(); };

    window.increment = (val) => incrementImpl(val);
})();
