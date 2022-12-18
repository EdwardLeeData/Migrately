import React, { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import Button from "react-bootstrap/Button";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";

const CheckoutSuccess = () => {
  const [paymentDetail, setPaymentDetail] = useState({});

  const navigate = useNavigate();
  const location = useLocation();
  useEffect(() => {
    if (location.state && location.state.type === "paymentSuccess") {
      setPaymentDetail(() => {
        return location.state.payload;
      });
    }
  }, [location.state]);

  const onHomeClicked = (e) => {
    e.preventDefault();
    navigate("/");
  };

  const onBackToCheckoutClicked = (e) => {
    e.preventDefault();
    navigate("/products");
  };

  return (
    <>
      <div className="bg-primary vh-100">
        <div className="d-flex align-items-stretch mx-auto justify-content-center">
          <Card className="border-0 align-items-center justify-content-center shadow-lg p-3 mt-5 mb-5 bg-white rounded">
            <Card.Img
              variant="top"
              src="https://trello.com/1/cards/637cf975e9493300153fa5d6/attachments/637cf98b3ceb22001caf8df7/download/image001.png"
              style={{ width: "50%", height: "50%" }}
            />
            <Card.Body>
              <Card.Title>
                <h2 className="text-center">
                  Thank you for your order,{" "}
                  {paymentDetail?.detail?.customerName}!
                </h2>
                <h3 className="text-center">
                  Invoice has been sent to your email!
                </h3>
              </Card.Title>

              <div className="d-flex justify-content-evenly mt-3">
                <div className="d-flex flex-column">
                  <h5> AMOUNT CHARGED </h5>
                  <p>${paymentDetail?.detail?.amount?.toFixed(2)} </p>
                </div>
                <div className="d-flex flex-column">
                  <h5> AMOUNT DUE </h5>
                  <p> ${paymentDetail?.detail?.amountReceived?.toFixed(2)} </p>
                </div>
                <div className="d-flex flex-column">
                  <h5> DATE POSTED </h5>
                  <p> {paymentDetail?.detail?.created?.split("T")[0]} </p>
                </div>
              </div>

              <ListGroup variant="flush">
                <h5>Summary</h5>
                <ListGroup.Item className="d-flex justify-content-between">
                  <p> {paymentDetail?.detail?.productName} </p>
                  <p> ${paymentDetail?.detail?.amount?.toFixed(2)} </p>
                </ListGroup.Item>
                <ListGroup.Item className="d-flex justify-content-between">
                  <p> Amount Due </p>
                  <p> ${paymentDetail?.detail?.amountReceived?.toFixed(2)}</p>
                </ListGroup.Item>
              </ListGroup>

              <h4>
                If you have any questions, please email{" "}
                <a href="mailto:support@migrately.com">support@migrately.com</a>
              </h4>

              <div className="d-flex mt-3">
                <ButtonGroup className="mx-auto">
                  <Button variant="outline-primary" onClick={onHomeClicked}>
                    Home
                  </Button>
                  <Button
                    variant="outline-primary"
                    onClick={onBackToCheckoutClicked}
                  >
                    Checkout
                  </Button>
                  <Button
                    variant="outline-primary"
                    href={paymentDetail?.invoice}
                  >
                    Download Invoice
                  </Button>
                </ButtonGroup>
              </div>
            </Card.Body>
          </Card>
        </div>
      </div>
    </>
  );
};

export default CheckoutSuccess;
