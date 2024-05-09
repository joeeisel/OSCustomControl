class virtuoso {
    signaturePad = null;

    info() {
        console.log('[virtuoso.info] BEGIN');

        const targetNode = document.getElementsByClassName('OSCustomControl.SignaturePad')[0];
        console.log('targetNode', targetNode)

        let _wrapper = document.getElementById("signature-pad");
        console.log(`_wrapper = ${_wrapper}`);

        //if (_wrapper) {
        //    let _canvas = _wrapper.querySelector("canvas");
        //    if (_canvas) {
        //        console.log(`_canvas = ${_canvas}`);
        //    } else {
        //        console.log(`NO _canvas found.`);
        //    }            
        //} else {
        //    console.log(`NO _wrapper found.`);
        //}

        console.log('[virtuoso.info] END');
    }

    setupSignature(signaturePadRef) {
        console.log('[virtuoso.setupSignature] BEGIN');
        try {

            //this.info();

            virtuoso.signaturePadRef = signaturePadRef;
            let _wrapper = document.getElementById("signature-pad");
            let _canvas = _wrapper.querySelector("canvas");

            //Some of this code was copied from this example and reference:
            //https://github.com/szimek/signature_pad/blob/master/docs/js/app.js

            virtuoso.signaturePad = new SignaturePad(_canvas, {
                // It's Necessary to use an opaque color when saving image as JPEG;
                // this option can be omitted if only saving as PNG or SVG
                backgroundColor: 'rgb(255, 255, 255)'
            });

            virtuoso.signaturePad.addEventListener("endStroke", (event) => {
                console.log('Signature end..');
                _canvas.toBlob(async (blob) => {
                    let buf = await blob.arrayBuffer();
                    let data = new Uint8Array(buf);
                    virtuoso.signaturePadRef.invokeMethodAsync("SetSignature", data);
                });
            });

            //resize canvas and clear
            if (virtuoso.signaturePad) {
                // When zoomed out to less than 100%, for some very strange reason,
                // some browsers report devicePixelRatio as less than 1
                // and only part of the canvas is cleared then.
                var ratio = Math.max(window.devicePixelRatio || 1, 1);

                let canvas = virtuoso.signaturePad.canvas;

                if (canvas) {
                    // This part causes the canvas to be cleared
                    canvas.width = canvas.offsetWidth * ratio;
                    canvas.height = canvas.offsetHeight * ratio;
                    canvas.getContext("2d").scale(ratio, ratio);
                }

                virtuoso.signaturePad.clear();
            }

        } catch (e) {
            console.error("Failed to setup Signature Pad: ", e);
        } finally {
            console.log('[virtuoso.setupSignature] END');
        }
    }

    cleanupSignature() {
        try {
            virtuoso.signaturePadRef = null;
            virtuoso.signaturePad.removeEventListener("endStroke", () => { });
            virtuoso.signaturePad = null;
        } catch (e) {
            console.error("Failed to cleanup Signature Pad: ", e);
        }
    }

    clearSignature() {
        try {
            if (virtuoso.signaturePad) {
                virtuoso.signaturePad.clear();
            }
        } catch (e) {
            console.error("Failed to clear Signature Pad: ", e);
        }
    }
}

window.virtuoso = new virtuoso();

//// Select the node that will be observed for mutations
////const targetNode = document.getElementById("signature-pad");
//const targetNode = document.getElementsByClassName('OSCustomControl.SignaturePad')[0];

//console.log('targetNode', targetNode);

//// Options for the observer (which mutations to observe)
//const config = { attributes: true, childList: true, subtree: true };

//// Callback function to execute when mutations are observed
//const callback = (mutationList, observer) => {
//    for (const mutation of mutationList) {
//        if (mutation.type === "childList") {
//            console.log("A child node has been added or removed.");
//        } else if (mutation.type === "attributes") {
//            console.log(`The ${mutation.attributeName} attribute was modified.`);
//        }
//    }
//};

//// Create an observer instance linked to the callback function
//const observer = new MutationObserver(callback);

//// Start observing the target node for configured mutations
//observer.observe(targetNode, config);
