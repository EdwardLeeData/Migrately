import React, { useState, useEffect } from "react";
import debug from "sabio-debug";
import PropTypes from "prop-types";
import { useNavigate } from "react-router-dom";
import ReactLoading from "react-loading";
import { Container, Row, Col } from "react-bootstrap";
import * as subscriptionService from "../../services/stripeSubscriptionService";

const CheckoutProcessing = () => {
  const _logger = debug.extend("CheckoutProcessing");

  const [paymentDetail, setPaymentDetail] = useState({
    detail: {},
    invoice: "",
  });

  const search = window.location.search;
  const params = new URLSearchParams(search);
  const sessionId = params.get("session_id");

  const navigate = useNavigate();

  const navigateToSuccessPage = () => {
    navigate("/products/success", {
      state: {
        payload: paymentDetail,
        type: "paymentSuccess",
      },
    });
  };

  useEffect(() => {
    setTimeout(() => {
      navigateToSuccessPage();
    }, 2000);
  }, [paymentDetail]);

  useEffect(() => {
    subscriptionService
      .GetPayloads(sessionId)
      .then(onGetPayloadSuccess)
      .catch(onGetPayloadError);
  }, []);

  const onGetPayloadSuccess = (response) => {
    _logger("Success session", response);
    const sessionPayload = response.data.item.sessionPayload;
    const invoice = response.data.item.invoiceDetail;

    setPaymentDetail((prevState) => {
      const pd = { ...prevState };
      pd.detail = sessionPayload;
      pd.invoice = invoice;
      return pd;
    });

    const subscriptionPayload = response.data.item.subscriptionPayload;
    subscriptionService
      .PostSubscriptionData(subscriptionPayload)
      .then(onPostSubscriptionDetailSuccess)
      .catch(onSubscriptionServiceError);
  };

  const onGetPayloadError = (error) => {
    _logger(error);
  };

  const onPostSubscriptionDetailSuccess = (response) => {
    _logger("post-->", response);
  };

  const onSubscriptionServiceError = (error) => {
    _logger(error);
  };

  return (
    <>
      <div className="bg-primary d-flex vh-100">
        <Container>
          <Row className="align-items-center">
            <Col>
              <div className="text-center mb-6 px-md-8 align-items-center">
                <img
                  src={
                    "https://trello.com/1/cards/637cf975e9493300153fa5d6/attachments/637cf98b3ceb22001caf8df7/download/image001.png"
                  }
                  alt="Logo"
                />
                <h1 className="text-white display-3 fw-bold">
                  Processing Payment
                </h1>
                <div className="d-flex justify-content-center ">
                  <ReactLoading
                    type={"bars"}
                    color={"white"}
                    height={50}
                    width={50}
                  />
                </div>
              </div>
            </Col>
          </Row>
        </Container>
      </div>
    </>
  );
};
CheckoutProcessing.propTypes = {
  currentUser: PropTypes.shape({
    id: PropTypes.number,
    email: PropTypes.string,
    isLoggedIn: PropTypes.bool,
  }).isRequired,
};
export default CheckoutProcessing;
