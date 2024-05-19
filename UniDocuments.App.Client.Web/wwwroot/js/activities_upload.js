$(".activity-upload-file-form").each((i, element) => {
    let submitButton = $(element).find(".activity-submit")
    let fileInput = $(element).find(".activity-file-input")

    fileInput.change(() => {
        const isDisabled = fileInput[0].value === "";
        submitButton.prop('disabled', isDisabled)
    })
})