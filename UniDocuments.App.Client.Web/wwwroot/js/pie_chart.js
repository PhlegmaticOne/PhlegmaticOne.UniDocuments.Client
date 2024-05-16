
function donutChart(data, targetElement) {
    Morris.Donut({
        element: targetElement,
        labelColor: "#ffffff",
        data: data,
        resize: true,
        redraw: true
    });
}