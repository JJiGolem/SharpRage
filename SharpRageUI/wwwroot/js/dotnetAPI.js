let dotnetEventsContainer = null;

window.setEventsContainer = (container) => {
    dotnetEventsContainer = container;
    console.info("Set dotnetEventsContainer");
}

window.callDotnet = (eventName, args) => {
    if (!dotnetEventsContainer) {
        console.error("CallDotnet: dotnetEventsContainer is null")
        return;
    }

    dotnetEventsContainer.invokeMethodAsync("Call", eventName, JSON.parse(args));
}

// Init rage mp module
(() => {
    if (typeof window.mp == 'undefined') {
        console.warn("This app is not started on rage mp!");
        return;
    }

    const mp = window.mp;

    console.debug("Rage MP object: ");
    console.debug(mp);

    mp.events.add("callEvent", (eventName, jsonArgs) => {
        console.debug(`${eventName}, args: {${jsonArgs}}`);
        window.callDotnet(eventName, jsonArgs);
    })
})();