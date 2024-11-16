const HomeController = function ($http, $scope, $async, $timeout) {

    $scope.filter_Lake_Setting = {
        lakeID: '-1'
    }

    const getData_Total_System_FishKoi = $async(async () => {
        try {
            const response = await $http.get("/Home/GetData_Total_System_FishKoi").then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                $scope.filter = response.Data;
            } else {
                toastr.warning(response.Message)
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi lấy dữ liệu thông số trong hệ thống');
        }
    })

    const getData_Lake_Total_FishKoi = $async(async () => {
        try {
            const response = await $http.get("/Home/GetData_Lake_Total_FishKoi").then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {

                const arrayCharts = response.Data.map(item => {
                    const objChart = {
                        Title: item.LakeName,
                        Data: item.Total_Quantity,
                        Record: item.Total_FishKoi
                    }
                    return objChart;
                })

                LOAD_CHART_HTML("report_lake_total_fishkoi", 'bar', arrayCharts, "Tổng số lượng");
            } else {
                toastr.warning(response.Message)
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi lấy dữ liệu tổng số cá trong hồ');
        }
    })

    let waterChartInstance = null;


    $scope.getData_AVG_Lake_Setting = $async(async () => {
        const params = {
            ...$scope.filter_Lake_Setting
        }

        const response = await $http.get("/Home/GetData_AVG_Lake_Setting", { params }).then(success => success.data);
        if (response.Status === MESSAGE_STATUS.success) {
            const arrayCharts = response.Data;

            if (waterChartInstance) {
                waterChartInstance.destroy();
            }

            const ctx = document.getElementById('waterChart').getContext('2d');
            waterChartInstance = new Chart(ctx, {
                type: 'line', // Loại biểu đồ (line, bar, etc.)
                data: {
                    labels: arrayCharts.map(item => item.MonthYear), // Tháng
                    datasets: [
                        {
                            label: 'Nhiệt độ',
                            data: arrayCharts.map(item => item.Temperature), // Dữ liệu nhiệt độ
                            borderColor: 'red',
                            fill: false
                        },
                        {
                            label: 'PH',
                            data: arrayCharts.map(item => item.PH), // Dữ liệu pH
                            borderColor: 'blue',
                            fill: false
                        },
                        {
                            label: 'O2',
                            data: arrayCharts.map(item => item.O2), // Dữ liệu độ cứng
                            borderColor: 'green',
                            fill: false
                        }
                        ,
                        {
                            label: 'NO2',
                            data: arrayCharts.map(item => item.NO2), // Dữ liệu độ cứng
                            borderColor: 'yellow',
                            fill: false
                        }
                        ,
                        {
                            label: 'Salt',
                            data: arrayCharts.map(item => item.Salt), // Dữ liệu độ cứng
                            borderColor: 'brown',
                            fill: false
                        }
                        ,
                        {
                            label: 'NO3',
                            data: arrayCharts.map(item => item.NO3), // Dữ liệu độ cứng
                            borderColor: 'pink',
                            fill: false
                        }
                        ,
                        {
                            label: 'PO4',
                            data: arrayCharts.map(item => item.PO4), // Dữ liệu độ cứng
                            borderColor: 'violet',
                            fill: false
                        }
                    ]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        x: {
                            title: {
                                display: true,
                                text: 'Month'
                            }
                        },
                        y: {
                            title: {
                                display: true,
                                text: 'Các thông số nồng độ'
                            }
                        }
                    }
                }
            });
        } else {
            toastr.warning(response.Message)
        }


    })

    const getAllLake = $async(async () => {
        try {
            const response = await $http.get("/Lake/Lake/GetAll").then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                $scope.list_Lake = response.Data;
            } else {
                toastr.warning(response.Message)
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi lấy dữ liệu hồ cá');
        }
    })

    const initData = () => {
        getAllLake();
        getData_Total_System_FishKoi();
        getData_Lake_Total_FishKoi();
        $scope.getData_AVG_Lake_Setting();
    }

    const LOAD_CHART_HTML = (id, chartConfig, list_Data_Chart, title) => {
        $timeout(() => {
            debugger;
            var ctx = document.getElementById(`${id}`);

            const labels = list_Data_Chart.map(x => x.Title);
            const list_Data = list_Data_Chart.map(x => x.Data);
            const list_Record = list_Data_Chart.map(x => x.Record);

            const data = {
                labels: labels,
                datasets: [{
                    record: list_Record,
                    data: list_Data,
                    label: title,
                    backgroundColor: [
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                    ],
                    borderColor: [
                        'rgb(75, 192, 192)',
                        'rgb(54, 162, 235)',
                    ],
                    borderWidth: 1
                }],
            };

            const config = {
                type: chartConfig, // Loại biểu đồ là pie chart
                data: data,
                options: {
                    plugins: {
                        tooltip: {
                            callbacks: {
                                label: function (tooltipItem) {
                                    if (chartConfig == 1) {
                                        var dataset = data.datasets[tooltipItem.datasetIndex];

                                        var total = dataset.data.reduce(function (previousValue, currentValue, currentIndex, array) {
                                            return previousValue + currentValue;
                                        });

                                        var currentValue = dataset.data[tooltipItem.dataIndex];
                                        var percentage = Math.round((currentValue / total) * 100) + "%";
                                        return data.labels[tooltipItem.dataIndex] + ': ' + percentage;
                                    }
                                },
                            },
                        },
                        datalabels: {
                            font: {
                                size: 14
                            },
                            color: '#78829D',
                            formatter: function (value, tooltipItem) {
                                var dataset = data.datasets[tooltipItem.datasetIndex];

                                var currentValue = dataset.record[tooltipItem.dataIndex];
                                return "Số loài cá trong hồ : " + currentValue
                                //var percentage = Math.round((currentValue / total) * 100) + "%";
                                //return percentage;
                            }
                        }

                    },

                    responsive: true,
                },
                plugins: [ChartDataLabels]
            };
            new Chart(ctx, config);
        })

    }


    initData();

}
HomeController.$inject = ["$http", "$scope", "$async", "$timeout"];