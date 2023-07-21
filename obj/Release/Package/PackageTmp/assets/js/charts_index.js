!function (o) { function r(t) { if (e[t]) return e[t].exports; var a = e[t] = { i: t, l: !1, exports: {} }; return o[t].call(a.exports, a, a.exports, r), a.l = !0, a.exports } var e = {}; r.m = o, r.c = e, r.d = function (o, e, t) { r.o(o, e) || Object.defineProperty(o, e, { configurable: !1, enumerable: !0, get: t }) }, r.n = function (o) { var e = o && o.__esModule ? function () { return o.default } : function () { return o }; return r.d(e, "a", e), e }, r.o = function (o, r) { return Object.prototype.hasOwnProperty.call(o, r) }, r.p = "", r(r.s = 15) }({
    15: function (o, r, e) { o.exports = e(16) }, 16: function (o, r) {
        function e(o, r) { return Math.floor(Math.random() * (r - o + 1)) + o }
        $(".doughnut-chart").each(function (o, r) {
            var e = r.getContext("2d"), t = r.dataset.percent, a = 100 - t, l = ""; switch (!0) { case t <= 25: l = colors.color_danger; break; case t <= 50: l = colors.color_warning; break; default: l = colors.color_success }
            new Chart(e, { type: "pie", data: { datasets: [{ data: [t, a], borderWidth: 1, backgroundColor: [l, colors.color_bg] }] }, options: { tooltips: { enabled: !1 }, cutoutPercentage: 85 } })
        }), $(".stats-chart").each(function (o, r) {
            for (var t = r.getContext("2d"), a = [], l = 6; l >= 0; l--) a.push(e(100, 300));
            new Chart(t,
                {
                    type: "line", data: {
                        labels: ["01", "02", "03", "04", "05", "06", "07", "08"],
                        datasets: [{ data: a, borderWidth: 2, borderColor: colors.color_primary, backgroundColor: "rgba(103, 116, 223,.12)", pointBackgroundColor: colors.color_primary }]
                    }, options: { elements: { point: { radius: 0 } }, tooltips: { enabled: !1 }, legend: { display: !1, labels: { display: !1 } }, scales: { xAxes: [{ gridLines: { display: !1, zeroLineColor: colors.border_color }, ticks: { display: !1 } }], yAxes: [{ gridLines: { display: !1, zeroLineColor: colors.border_color }, ticks: { display: !1 } }] } }
                })
        }),

        new Chart($("#dashboard-chart")[0].getContext("2d"), {
            type: "line", data: {
                labels: ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13"],
                datasets: [{ label: "Sales", data: [2e3, 1e3, 2e3, 4e3, 5e3, 6e3, 8e3, 4e3, 5e3, 6e3, 2e3, 3e3, 4e3], borderWidth: 2, borderColor: colors.color_primary, backgroundColor: colors.color_bg, pointBackgroundColor: colors.color_primary }]
            }, options: { maintainAspectRatio: !1, legend: { display: !1, labels: { display: !1 } }, tooltips: { mode: "index", callbacks: { footer: function (o, r) { var e = 0; return o.forEach(function (o) { e += r.datasets[o.datasetIndex].data[o.index] }), "Sum: " + e } }, footerFontStyle: "normal" }, scales: { yAxes: [{ stacked: !0, gridLines: { color: colors.border_color, zeroLineColor: colors.border_color }, ticks: { callback: function (o, r, e) { return parseInt(o) >= 1e3 ? "$" + o.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : "$" + o } } }], xAxes: [{ gridLines: { display: !1 }, border: { display: !0, color: colors.border_color }, ticks: { beginAtZero: !0 } }] } }
        }), $("#visitors-chart").length &&

        new Chart($("#visitors-chart"), {
            type: "pie", data: {
                datasets: [{ data: [12, 30, 15, 13, 20, 10], borderWidth: 1, backgroundColor: [colors.color_bg, colors.color_primary, colors.color_warning, colors.color_danger, colors.color_blue, colors.color_success] }]
            }, options: { tooltips: { enabled: !0 }, cutoutPercentage: 40, events: !1, animation: { duration: 500, easing: "easeOutQuart", onComplete: function () { var o = this.chart.ctx; o.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontFamily, "normal", Chart.defaults.global.defaultFontFamily), o.textAlign = "center", o.textBaseline = "bottom", this.data.datasets.forEach(function (r) { for (var e = 0; e < r.data.length; e++) { var t = r._meta[Object.keys(r._meta)[0]].data[e]._model, a = r._meta[Object.keys(r._meta)[0]].total, l = t.innerRadius + (t.outerRadius - t.innerRadius) / 2, n = t.startAngle, s = t.endAngle, c = n + (s - n) / 2, i = l * Math.cos(c), d = l * Math.sin(c); o.fillStyle = "#fff", 0 == e && (o.fillStyle = "#444"); var u = String(Math.round(r.data[e] / a * 100)) + "%"; o.fillText(u, t.x + i, t.y + d + 10) } }) } } }
        }),

        new Chart($("#members-chart")[0].getContext("2d"), {
            type: "bar", data: {
                labels: ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"],
                datasets: [{ label: "Sales", data: [401, 362, 210, 85, 105, 125, 90, 150, 540, 980, 1102, 1150], borderWidth: 5, borderColor: colors.color_primary, backgroundColor: colors.color_primary }]
            }, options: { maintainAspectRatio: !1, legend: { display: !1, labels: { display: !1 } }, scales: { yAxes: [{ stacked: !0, gridLines: { color: colors.border_color, zeroLineColor: colors.border_color }, ticks: { callback: function (o, r, e) { return parseInt(o) >= 1e3 ? "$" + o.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : "$" + o } } }], xAxes: [{ gridLines: { display: !1 } }] } }
        })
    }
});